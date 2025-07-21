using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Areas.EmployeeArea.Controllers
{
    [Area("EmployeeArea")]
    public class ContractController : Controller
    {
       
        public IActionResult Index()
        {
            var contracts = ContractRepository.Instance.GetByEmployeeId(18);
            var ctrTest = ContractRepository.Instance.Test();
            return View(contracts);
        }
        public IActionResult Details(int id)
        {
            //var contract = ContractRepository.Instance.FindById(id);
            //if (contract == null)
            //{
            //    return NotFound();
            //}
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
