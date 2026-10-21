using Insurance_agency.Services;
using Insurance_agency.Models.ModelView;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Controllers;

public class PaymentController : Controller
{
    private readonly CheckoutService _checkoutService;

    public PaymentController(CheckoutService checkoutService)
    {
        _checkoutService = checkoutService;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePaymentUrlVnpay()
    {
        var user = HttpContext.Session.GetObject<User>("user");
        if (user is null)
        {
            return RedirectToAction("Index", "Login");
        }

        try
        {
            var url = await _checkoutService.CreatePaymentUrlAsync(HttpContext, user.id);
            return Redirect(url);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
