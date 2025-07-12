using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
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
            var insurance = InsuranceRepository.Instance.FindById(id);
            var user = HttpContext.Session.GetObject<User>("user");
          


            ContractView contractView = new ContractView();
            contractView.insurance_id = insurance.id;
            contractView.user_id = user?.id ?? 0;
            contractView.StartDate = DateTime.Now;
            contractView.EndDate = DateTime.Now.AddYears(insurance.year_max);
            contractView.value_contract = insurance.value;
            contractView.year_paid  = insurance.year_max;

            HttpContext.Session.SetObject("contract", contractView);



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
