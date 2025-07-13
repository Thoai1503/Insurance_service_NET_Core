using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {

            HttpContext.Session.Remove("contract");
            HttpContext.Session.Remove("user");

            return RedirectToAction("Index", "Home");
        }
    }
}
