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
          
            if (user == null)
            {
                // Redirect to login page if user is not logged in
                return RedirectToAction("Index", "Login");
            }

            ContractView contractView = new ContractView();
            contractView.insurance_id = insurance.id;
            contractView.user_id = user.id ;
            contractView.StartDate = DateTime.Now;
            contractView.EndDate = DateTime.Now.AddYears(insurance.year_max);
            contractView.value_contract = insurance.value;
            contractView.year_paid  = insurance.value / insurance.year_max;
            contractView.number_year_paid = insurance.year_max;

            HttpContext.Session.SetObject("contract", contractView);    



            return View();
        }
        [HttpGet]
        public IActionResult PaymentCallbackVnpay()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            if(response.Success)
            {
                var contract = HttpContext.Session.GetObject<ContractView>("contract");
                contract.total_paid = contract.year_paid / contract.number_year_paid;
                // Save the contract to the database or perform any necessary actions
       //         contract.total_paid = response.
                
                var success= ContractRepository.Instance.Create(contract);
                // Handle successful payment
             
                if (success)
                {
                    HttpContext.Session.Remove("contract");
                    // Redirect to a success page or display a success message
                    return  RedirectToAction("Successful", "Checkout");
                    
              
                }
                else
                {
                  
                    // Handle the case where saving the contract failed
                    ViewBag.Message = "Payment successful but failed to save the contract!";
                    // You can redirect to an error page or display an error message
                    Content("Payment successful but failed to save the contract!");
                }
            }
            else
            {
                // Handle failed payment
                // You can redirect to a failure page or display an error message
                ViewBag.Message = "Payment failed!";
            }

            return Json(response);
        }
        public IActionResult Successful()
        {

            HttpContext.Session.SetInt32("allbanner", 0);
            // This action can be used to display a success message or redirect to a success page
            ViewBag.Message = "Payment was successful!";
            return View();
        }
        public IActionResult Test()

        {
            HttpContext.Session.SetInt32("allbanner", 0);
            return View();
        }


    }
}
