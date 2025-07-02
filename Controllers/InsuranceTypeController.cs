using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Controllers
{
    public class InsuranceTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            var insuranceTypes = InsuranceTypeRepository.Instance.GetAll();

            return PartialView("_InsuranceList",insuranceTypes);
        }
    }
}
