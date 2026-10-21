using System.Data;
using Insurance_agency.Models.Entities;
using Insurance_agency.Models.ModelView;
using Microsoft.EntityFrameworkCore;

namespace Insurance_agency.Models.Repository;

public sealed class CheckoutRepository
{
    private readonly InsuranceContext _context;

    public CheckoutRepository(InsuranceContext context)
    {
        _context = context;
    }

    public Task<InsuranceView?> GetActiveInsuranceAsync(int insuranceId)
    {
        return _context.Insurances
            .Where(insurance => insurance.Id == insuranceId && insurance.Status == 1)
            .Select(insurance => new InsuranceView
            {
                id = insurance.Id,
                name = insurance.Name ?? string.Empty,
                description = insurance.Description ?? string.Empty,
                year_max = insurance.YearMax ?? 0,
                value = insurance.Value ?? 0,
                insurance_type_id = insurance.InsuranceTypeId ?? 0,
                ex_image = insurance.ExImage ?? string.Empty
            })
            .FirstOrDefaultAsync();
    }

    public Task<ContractView?> GetContractForUserAsync(int contractId, int userId)
    {
        return _context.TblContracts
            .Where(contract => contract.Id == contractId && contract.UserId == userId)
            .Select(contract => new ContractView
            {
                id = contract.Id,
                user_id = contract.UserId,
                insurance_id = contract.InsuranceId,
                StartDate = contract.StartDate ?? DateTime.MinValue,
                EndDate = contract.EndDate ?? DateTime.MinValue,
                total_paid = (long?)contract.TotalPaid,
                year_paid = (long?)contract.YearPaid,
                number_year_paid = contract.NumberYearPaid,
                value_contract = (long?)contract.ValueContract,
                status = contract.Status
            })
            .FirstOrDefaultAsync();
    }

    public Task<bool> HasCompletedTransactionAsync(string transactionReference, long amount)
    {
        return _context.TblPaymentHistories.AnyAsync(payment =>
            payment.VnpayTransactionRef == transactionReference &&
            payment.Amount == amount &&
            payment.Status == 1);
    }

    public async Task<bool> CreateContractAndPaymentAsync(
        ContractView contract,
        long amount,
        string transactionReference)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
        try
        {
            if (await _context.TblPaymentHistories.AnyAsync(
                    payment => payment.VnpayTransactionRef == transactionReference))
            {
                await transaction.CommitAsync();
                return true;
            }

            var contractEntity = new TblContract
            {
                UserId = contract.user_id,
                InsuranceId = contract.insurance_id,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                ValueContract = contract.value_contract,
                YearPaid = contract.year_paid,
                NumberYearPaid = contract.number_year_paid,
                TotalPaid = amount,
                Status = contract.status,
                EmployeeId = contract.employee_id
            };
            _context.TblContracts.Add(contractEntity);
            await _context.SaveChangesAsync();

            _context.TblPaymentHistories.Add(new TblPaymentHistory
            {
                ContractId = contractEntity.Id,
                Amount = amount,
                PaymentDay = DateTime.Now,
                Status = 1,
                VnpayTransactionRef = transactionReference
            });
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> AddInstallmentPaymentAsync(
        int contractId,
        int userId,
        long amount,
        string transactionReference)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
        try
        {
            if (await _context.TblPaymentHistories.AnyAsync(
                    payment => payment.VnpayTransactionRef == transactionReference))
            {
                await transaction.CommitAsync();
                return true;
            }

            var contract = await _context.TblContracts
                .SingleOrDefaultAsync(item => item.Id == contractId && item.UserId == userId);
            if (contract is null)
            {
                throw new InvalidOperationException("Không tìm thấy hợp đồng thuộc về tài khoản hiện tại.");
            }

            var remaining = (long)(contract.ValueContract ?? 0) - (long)(contract.TotalPaid ?? 0);
            if (amount <= 0 || amount > remaining)
            {
                throw new InvalidOperationException("Số tiền thanh toán không hợp lệ.");
            }

            contract.TotalPaid = (contract.TotalPaid ?? 0) + amount;
            if (contract.TotalPaid >= (contract.ValueContract ?? 0))
            {
                contract.Status = 2;
            }

            _context.TblPaymentHistories.Add(new TblPaymentHistory
            {
                ContractId = contract.Id,
                Amount = amount,
                PaymentDay = DateTime.Now,
                Status = 1,
                VnpayTransactionRef = transactionReference
            });
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
