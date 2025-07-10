using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            var employees = UserRepository.Instance.GetAllEmployeeUser();
            return View(employees);
        }
    }
}
