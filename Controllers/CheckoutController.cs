using Insurance_agency.Models.ModelView;
using Insurance_agency.Services.VnPay;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IVnPayService _vnPayService;
        public CheckoutController(IVnPayService vnPayService)
        {

            _vnPayService = vnPayService;
        }
        public IActionResult Index(int id)
        {
        
            // Convert insurance variable to InsuranceView model or any other model you need
          

            return View();
        }
        [HttpGet]
        public IActionResult PaymentCallbackVnpay()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            if(response.Success)
            {
                // Handle successful payment
                // You can redirect to a success page or display a success message
                ViewBag.Message = "Payment successful!";
            }
            else
            {
                // Handle failed payment
                // You can redirect to a failure page or display an error message
                ViewBag.Message = "Payment failed!";
            }

            return Json(response);
        }



    }
}
