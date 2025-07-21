using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Areas.EmployeeArea.Controllers
{
    public class ContractController : Controller
    {
        [Area("EmployeeArea")]
        public IActionResult Index()
        {
            var contracts = ContractRepository.Instance.GetByEmployeeId(7);
            var ctrTest = ContractRepository.Instance.Test();
            return View(contracts);
        }
    }
}
