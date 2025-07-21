using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            var customers = UserRepository.Instance.GetCustomerUser();
            return View(customers);
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
