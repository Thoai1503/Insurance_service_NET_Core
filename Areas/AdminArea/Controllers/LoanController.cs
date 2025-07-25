using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class LoanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
