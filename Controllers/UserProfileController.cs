using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Controllers
{
    public class UserProfileController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetObject<User>("user") == null)
            {
                return RedirectToAction("Index","Login");
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
            HttpContext.Session.SetInt32("allbanner", 0);
            return View();
        }
    }
}
