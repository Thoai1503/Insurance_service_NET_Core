using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Areas.AdminArea.Controllers
{
    public class EarningController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
