using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Insurance_agency.Services.VnPay;

namespace Insurance_agency.Services;

public sealed class CheckoutService
{
    private const string NewContractSessionKey = "contract";
    private const string ExistingContractSessionKey = "contractNextPayment";
    private const string ExistingAmountSessionKey = "amount";
    private const string PendingPaymentSessionKey = "pendingPayment";

    private readonly CheckoutRepository _repository;
    private readonly IVnPayService _vnPayService;

    public CheckoutService(CheckoutRepository repository, IVnPayService vnPayService)
    {
        _repository = repository;
        _vnPayService = vnPayService;
    }

    public async Task<(InsuranceView Insurance, ContractView Contract)?> CreateContractDraftAsync(
        int insuranceId,
        int userId)
    {
        var insurance = await _repository.GetActiveInsuranceAsync(insuranceId);
        if (insurance is null)
        {
            return null;
        }

        if (insurance.year_max <= 0 || insurance.value <= 0)
        {
            throw new InvalidOperationException("Thông tin thời hạn hoặc giá bảo hiểm không hợp lệ.");
        }

        var startDate = DateTime.Now;
        var contract = new ContractView
        {
            insurance_id = insurance.id,
            user_id = userId,
            StartDate = startDate,
            EndDate = startDate.AddYears(insurance.year_max),
            value_contract = insurance.value,
            year_paid = (long)Math.Ceiling((decimal)insurance.value / insurance.year_max),
            number_year_paid = insurance.year_max,
            total_paid = 0,
            status = 0
        };

        return (insurance, contract);
    }

    public async Task<ContractView?> CreateInstallmentDraftAsync(int contractId, int userId, int amount)
    {
        var contract = await _repository.GetContractForUserAsync(contractId, userId);
        if (contract is null)
        {
            return null;
        }

        var remaining = (contract.value_contract ?? 0) - (contract.total_paid ?? 0);
        if (amount <= 0 || amount > remaining)
        {
            throw new InvalidOperationException("Số tiền thanh toán không hợp lệ.");
        }

        return contract;
    }

    public async Task<string> CreatePaymentUrlAsync(
        HttpContext httpContext,
        int userId)
    {
        var session = httpContext.Session;
        var newContract = session.GetObject<ContractView>(NewContractSessionKey);
        var existingContract = session.GetObject<ContractView>(ExistingContractSessionKey);
        PendingPayment pending;

        if (newContract is not null)
        {
            if (newContract.user_id != userId || newContract.insurance_id is null ||
                newContract.year_paid is null or <= 0)
            {
                throw new InvalidOperationException("Thông tin hợp đồng thanh toán không hợp lệ.");
            }

            pending = new PendingPayment
            {
                OrderId = Guid.NewGuid().ToString("N"),
                UserId = userId,
                Amount = newContract.year_paid.Value,
                NewContract = newContract
            };
        }
        else if (existingContract is not null)
        {
            var amount = session.GetInt32(ExistingAmountSessionKey);
            if (existingContract.id <= 0 || amount is null or <= 0)
            {
                throw new InvalidOperationException("Thông tin thanh toán hợp đồng không hợp lệ.");
            }

            pending = new PendingPayment
            {
                OrderId = Guid.NewGuid().ToString("N"),
                UserId = userId,
                Amount = amount.Value,
                ExistingContractId = existingContract.id
            };
        }
        else
        {
            throw new InvalidOperationException("Không tìm thấy thông tin thanh toán.");
        }

        var validatedContract = pending.NewContract is null
            ? await _repository.GetContractForUserAsync(pending.ExistingContractId!.Value, userId)
            : pending.NewContract;
        if (validatedContract is null || validatedContract.user_id != userId)
        {
            throw new InvalidOperationException("Không tìm thấy hợp đồng thuộc về tài khoản hiện tại.");
        }

        if (pending.ExistingContractId.HasValue)
        {
            var remaining = (validatedContract.value_contract ?? 0) - (validatedContract.total_paid ?? 0);
            if (pending.Amount <= 0 || pending.Amount > remaining)
            {
                throw new InvalidOperationException("Số tiền thanh toán không hợp lệ.");
            }
        }

        session.SetObject(PendingPaymentSessionKey, pending);
        var payment = new PaymentInformationModel
        {
            Name = "Insurance",
            OrderDescription = pending.ExistingContractId.HasValue
                ? $"Thanh toán hợp đồng {pending.ExistingContractId.Value}"
                : "Thanh toán hợp đồng bảo hiểm",
            OrderType = "other",
            Amount = pending.Amount,
            OrderId = pending.OrderId
        };
        return _vnPayService.CreatePaymentUrl(payment, httpContext);
    }

    public async Task<bool> CompletePaymentAsync(
        PaymentResponseModel response,
        HttpContext httpContext,
        int userId)
    {
        var pending = httpContext.Session.GetObject<PendingPayment>(PendingPaymentSessionKey);
        if (!response.Success || response.VnPayResponseCode != "00")
        {
            return false;
        }

        if (pending is null)
        {
            return await _repository.HasCompletedTransactionAsync(response.OrderId, response.Amount);
        }

        if (pending.UserId != userId ||
            !string.Equals(pending.OrderId, response.OrderId, StringComparison.Ordinal) ||
            pending.Amount != response.Amount)
        {
            return false;
        }

        var saved = pending.NewContract is not null
            ? await _repository.CreateContractAndPaymentAsync(
                pending.NewContract,
                pending.Amount,
                pending.OrderId)
            : await _repository.AddInstallmentPaymentAsync(
                pending.ExistingContractId!.Value,
                pending.UserId,
                pending.Amount,
                pending.OrderId);

        if (saved)
        {
            httpContext.Session.Remove(PendingPaymentSessionKey);
            httpContext.Session.Remove(NewContractSessionKey);
            httpContext.Session.Remove(ExistingContractSessionKey);
            httpContext.Session.Remove(ExistingAmountSessionKey);
        }

        return saved;
    }

    public Task<bool> ProcessPaymentCallbackAsync(HttpContext httpContext, int userId)
    {
        var response = _vnPayService.PaymentExecute(httpContext.Request.Query);
        return CompletePaymentAsync(response, httpContext, userId);
    }
}
