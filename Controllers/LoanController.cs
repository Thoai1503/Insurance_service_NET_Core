using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Controllers
{
    public class LoanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RequestForm(int contractId)
        {
            var contract = ContractRepository.Instance.FindById(contractId);
            return View(contract);
        }
    }
}
