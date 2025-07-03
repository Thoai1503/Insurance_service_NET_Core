using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Areas.UserArea.Controllers
{
    [Area("UserArea")]
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
