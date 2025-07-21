using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Areas.EmployeeArea.Controllers
{
    public class DashboardController : Controller
    {
        [Area("EmployeeArea")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
