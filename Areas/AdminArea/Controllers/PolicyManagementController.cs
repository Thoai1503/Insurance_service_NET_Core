using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Areas.AdminArea.Controllers
{
    public class PolicyManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
