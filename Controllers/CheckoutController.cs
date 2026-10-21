using Insurance_agency.Models.ModelView;
using Insurance_agency.Services;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Controllers;

public class CheckoutController : Controller
{
    private readonly CheckoutService _checkoutService;

    public CheckoutController(CheckoutService checkoutService)
    {
        _checkoutService = checkoutService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int id)
    {
        HttpContext.Session.SetInt32("allbanner", 0);
        var user = HttpContext.Session.GetObject<User>("user");
        if (user is null)
        {
            return RedirectToAction("Index", "Login");
        }

        var draft = await _checkoutService.CreateContractDraftAsync(id, user.id);
        if (draft is null)
        {
            return NotFound();
        }

        HttpContext.Session.Remove("contractNextPayment");
        HttpContext.Session.Remove("amount");
        HttpContext.Session.Remove("pendingPayment");
        HttpContext.Session.SetObject("contract", draft.Value.Contract);
        return View(draft.Value.Insurance);
    }

    [HttpGet]
    public async Task<IActionResult> PaymentCallbackVnpay()
    {
        var user = HttpContext.Session.GetObject<User>("user");
        if (user is null)
        {
            return Unauthorized("Không tìm thấy phiên đăng nhập để xác nhận giao dịch.");
        }

        bool completed;
        try
        {
            completed = await _checkoutService.ProcessPaymentCallbackAsync(HttpContext, user.id);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }

        if (!completed)
        {
            return BadRequest("Không thể xác nhận giao dịch. Vui lòng liên hệ bộ phận hỗ trợ.");
        }

        return RedirectToAction(nameof(Successful));
    }

    [HttpGet]
    public IActionResult Successful()
    {
        HttpContext.Session.SetInt32("allbanner", 0);
        ViewBag.Message = "Payment was successful!";
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Test(int amount, int contractId)
    {
        var user = HttpContext.Session.GetObject<User>("user");
        if (user is null)
        {
            return RedirectToAction("Index", "Login");
        }

        var contract = await _checkoutService.CreateInstallmentDraftAsync(contractId, user.id, amount);
        if (contract is null)
        {
            return NotFound("Không tìm thấy hợp đồng thuộc về tài khoản hiện tại.");
        }

        HttpContext.Session.Remove("contract");
        HttpContext.Session.Remove("pendingPayment");
        HttpContext.Session.SetObject("contractNextPayment", contract);
        HttpContext.Session.SetInt32("amount", amount);

        HttpContext.Session.SetInt32("allbanner", 0);
        ViewBag.Amount = amount;
        ViewBag.ContractId = contractId;
        return View(contract);
    }
}
