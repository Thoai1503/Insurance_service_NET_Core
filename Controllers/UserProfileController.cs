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
            if (HttpContext.Session.GetObject<User>("user") == null)
            {
                return RedirectToAction("Index", "Login");
            }
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
            if (HttpContext.Session.GetObject<User>("user") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var user = HttpContext.Session.GetObject<User>("user");
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var totalAmount = 0;
            var contracts = ContractRepository.Instance.GetContractsByUserId(user.id);

            ViewBag.Contracts = contracts;
            HttpContext.Session.SetInt32("allbanner", 0);
            return View(contracts);
        }
        public IActionResult PaymentHistory(int contractId)
        {
            if (HttpContext.Session.GetObject<User>("user") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var payment = PaymentRepository.Instance.FindByContractId(contractId).OrderByDescending(c => c.id).ToHashSet();
            var paymentCount = payment.Count;


            var contract = ContractRepository.Instance.FindById(contractId);

            //Estimate the next payment year

            string nextDueDate = contract.StartDate?.AddYears(paymentCount).ToString();

            DateTime date = DateTime.ParseExact(nextDueDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            // Set time to midnight
            var displayDate = date.ToString("dd MMM yyyy");
            var password = BCrypt.Net.BCrypt.HashPassword("Thoaivip13");

            HttpContext.Session.SetInt32("allbanner", 0);
            ViewBag.NextDueDate = displayDate;
            ViewBag.Id = contractId;
            ViewBag.Contract = contract;
            return View(payment);
        }
        public IActionResult AllPaymentHistory()
        {
            if (HttpContext.Session.GetObject<User>("user") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var user = HttpContext.Session.GetObject<User>("user");
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var contracts = ContractRepository.Instance.GetContractsByUserId(user.id);
            ViewBag.Contracts = contracts;
            HttpContext.Session.SetInt32("allbanner", 0);
            return View(contracts);
        }
        public IActionResult LogOut()
        {
            try
            {
                if (HttpContext.Session.GetObject<User>("user") != null)
                {
                    HttpContext.Session.Remove("user");
                }

            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("index", "Login");
        }
        public IActionResult contractInfo(int contractId = 0)
        {
            try
            {
                if (HttpContext.Session.GetObject<User>("user") != null)
                {
                    if (contractId == 0)
                    {
                        return RedirectToAction("index");
                    }
                    var contract = ContractRepository.Instance.FindById(contractId);
                    ViewBag.contract = contract;
                }
                HttpContext.Session.SetInt32("allbanner", 0);
            }
            catch (Exception ex)
            {
            }
            return View();
        }
    }
}
