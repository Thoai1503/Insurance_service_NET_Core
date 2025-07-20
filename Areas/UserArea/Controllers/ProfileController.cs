using Insurance_agency.Models.ModelView;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Areas.UserArea.Controllers
{
    [Area("UserArea")]
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            var user = HttpContext.Session.GetObject<User>("user");
            ViewBag.user = user;
            return View();
        }

    }
}
