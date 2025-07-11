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
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult EmployeeDetail(int id)
        {
            var employee = UserRepository.Instance.FindById(id);
            var contracts = ContractRepository.Instance.FindByEmployeeId(id);
            ViewBag.Contracts = contracts;
            return View(employee);
        }
    }
}
