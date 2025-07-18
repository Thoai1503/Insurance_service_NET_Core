using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Controllers
{
    public class LoanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RequestForm()
        {
            return View();
        }
    }
}
