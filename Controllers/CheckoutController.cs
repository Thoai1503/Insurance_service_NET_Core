using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Insurance_agency.Services.VnPay;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;

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

            HttpContext.Session.SetInt32("allbanner", 0);
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



            return View(insurance);
        }
        [HttpGet]
        public IActionResult PaymentCallbackVnpay()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            if(response.Success)
            {
                var contract = HttpContext.Session.GetObject<ContractView>("contract");
                if (contract != null)
                {
                    contract.total_paid = (long)contract.year_paid / (long)contract.number_year_paid;
                    contract.status = 0; // Assuming 1 means active or in progress
                    // Save the contract to the database or perform any necessary actions
                    //         contract.total_paid = response.

                    var success = ContractRepository.Instance.CreateReturnId(contract);
                    // Handle successful payment

                    if (success>0)
                    {
                        var paymentHistory = new PaymentHistory
                        {
                            contract_id = success,
                            amount = (long)contract.year_paid,
                            payment_day = DateTime.Now,
                            status = 0 // Assuming 1 means successful
                        };
                        var paymentSuccess = PaymentRepository.Instance.Create(paymentHistory);
                        if (!paymentSuccess)
                        {
                            // Handle the case where saving the payment history failed
                            ViewBag.Message = "Payment successful but failed to save the payment history!";
                            // You can redirect to an error page or display an error message
                            Content("Payment successful but failed to save the payment history!");
                        }
                        HttpContext.Session.Remove("contract");
                        // Redirect to a success page or display a success message
                        return RedirectToAction("Successful", "Checkout");


                    }
                    else
                    {

                       
                        // You can redirect to an error page or display an error message
                        Content("Payment successful but failed to save the contract!");
                    }
                }
                else
                {

                    var contractNextPayment = HttpContext.Session.GetObject<ContractView>("contractNextPayment");
                    var amount = HttpContext.Session.GetInt32("amount");
                    // Update total paid amount in the contract
                    contractNextPayment.total_paid = (long)(contractNextPayment.total_paid + amount);
                    var success = ContractRepository.Instance.Update(contractNextPayment);
                    var contractId = contractNextPayment.id;
                    var cntr= ContractRepository.Instance.FindById(contractId);
                    if(cntr.total_paid ==cntr.value_contract)
                        {
                        cntr.status = 2; // Assuming 2 means completed
                        ContractRepository.Instance.Update(cntr);
                    }

                    // Save the payment history (payment_histoy table)

                    PaymentHistory paymentHistory = new PaymentHistory
                    {
                        contract_id = contractNextPayment.id,
                        amount = amount.Value,
                        payment_day = DateTime.Now,
                        status = 1 // Assuming 1 means successful
                    };
                    var paymentSuccess = PaymentRepository.Instance.Create(paymentHistory);
                 
                    if (!paymentSuccess)
                    {
                        // Handle the case where saving the payment history failed
                        ViewBag.Message = "Payment successful but failed to save the payment history!";
                        // You can redirect to an error page or display an error message
                        Content("Payment successful but failed to save the payment history!");
                    }
                    HttpContext.Session.Remove("contractNextPayment");
                    HttpContext.Session.Remove("amount");
                


                    return RedirectToAction("Successful", "Checkout");
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
        public IActionResult Test(int amount, int contractId, InsuranceView insurance)
        {
            var contract = ContractRepository.Instance.FindById(contractId);

            if (contract == null)
            {
                return NotFound("Contract not found.");
            }

            HttpContext.Session.SetObject("contractNextPayment", contract);
            HttpContext.Session.SetInt32("amount", amount);

            HttpContext.Session.SetInt32("allbanner", 0);
            ViewBag.Amount = amount;
            ViewBag.ContractId = contractId;
            return View(contract);
        }


    }
}
