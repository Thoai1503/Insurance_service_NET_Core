using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;

namespace Insurance_agency.Controllers
{
    public class UserProfileController : Controller
    {
        public IActionResult Index()
        {
            var user = HttpContext.Session.GetObject<User>("user");
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            HttpContext.Session.SetInt32("allbanner", 0);
            return View(user);
        }
        public IActionResult ContractHistory()
        {

            var user = HttpContext.Session.GetObject<User>("user");
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var totalAmount = 0;
            var contracts = ContractRepository.Instance.GetContractsByUserId(user.id);
          var totalPaid = contracts.Sum(c => c.total_paid);
            var contrNum = contracts.Count();
           DateTime nextDue = contracts.Min(c=>c.next_payment_due);
            var displayDate = nextDue.ToString("dd MMM yyyy");
            ContractView nextDueContract = contracts.Where(c => c.next_payment_due == nextDue).FirstOrDefault();
            ViewBag.TotalPaid = totalPaid;
             ViewBag.Contracts = contracts;
            ViewBag.Count = contrNum;
            ViewBag.NextDue = displayDate;
            ViewBag.NextDueContract = nextDueContract;
            HttpContext.Session.SetInt32("allbanner", 0);
            return View(contracts);
        }
        public IActionResult PaymentHistory(int contractId)
        {
            var payment = PaymentRepository.Instance.FindByContractId(contractId).OrderByDescending(c=>c.id).ToHashSet();
            var paymentCount = payment.Count;
 

            var contract = ContractRepository.Instance.FindById(contractId);

            var remainPayment = contract.number_year_paid - paymentCount;
            //Estimate the next payment year
       
            string nextDueDate = contract.StartDate?.AddYears(paymentCount).ToString();

            DateTime date = DateTime.ParseExact(nextDueDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
          // Set time to midnight
            var displayDate = date.ToString("dd MMM yyyy");
            var password = BCrypt.Net.BCrypt.HashPassword("Thoaivip13");

            HttpContext.Session.SetInt32("allbanner", 0);
            ViewBag.NextDueDate = displayDate;
            ViewBag.Id = contractId;
            ViewBag.RemainTime = remainPayment;
            ViewBag.Contract = contract;
            return View(payment);
        }
    }
}
