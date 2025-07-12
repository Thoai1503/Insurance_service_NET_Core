using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
