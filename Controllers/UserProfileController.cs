using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Controllers
{
    public class UserProfileController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.SetInt32("allbanner", 0);
            return View();
        }
    }
}
