using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;

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
            var contracts = ContractRepository.Instance.GetContractsByUserId(user.id);
            ViewBag.Contracts = contracts;
            HttpContext.Session.SetInt32("allbanner", 0);
            return View(contracts);
        }
        public IActionResult PaymentHistory(int contractId)
        {
            var payment = PaymentRepository.Instance.FindByContractId(contractId).OrderByDescending(c=>c.id).ToHashSet();
            var contract = ContractRepository.Instance.FindById(contractId);



            HttpContext.Session.SetInt32("allbanner", 0);
            ViewBag.Id = contractId;
            ViewBag.Contract = contract;
            return View(payment);
        }
    }
}
