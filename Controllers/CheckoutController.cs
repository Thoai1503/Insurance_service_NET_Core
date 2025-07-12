using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
