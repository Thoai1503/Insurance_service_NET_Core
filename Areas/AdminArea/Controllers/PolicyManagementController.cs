using Insurance_agency.Models.Entities;
using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class PolicyManagementController : Controller
    {
        private readonly InsuranceContext _context;
        public PolicyManagementController()
        {
            _context = new InsuranceContext();
        }
        public IActionResult Index()
        {
            var poli = PolicyRepository.Instance.GetAll();
            var poliWithInsurance = poli.Select(p =>
            {
                var insurance = _context.Insurances.FirstOrDefault(i => i.Id == p.insurance_id);
                return new
                {
                    p.id,
                    p.name,
                    p.description,
                    p.age_max,
                    p.age_min,
                    p.active,
                    insurance_name = insurance?.Name ?? ""
                };
            }).ToList();
            ViewBag.Policies = poliWithInsurance;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Policy en)
        {

            var rs = PolicyRepository.Instance.Create(en);

            if (rs)
            {
                return Ok(true);
            }
            else
            {
                // Handle the case where creation failed, e.g., return an error message
                return Ok(false);
            }

        }
   
        public IActionResult Delete(int id)
        {
            var rs = PolicyRepository.Instance.Delete(id);
            if (rs)
            {
                return Ok(rs);
            }
            else
            {
                // Handle the case where deletion failed, e.g., return an error message
                return Ok(false);
            }
        }
        public IActionResult Details(int id)
        {
            var poli = PolicyRepository.Instance.FindById(id);
            if (poli == null) return NotFound();
            var insu = _context.Insurances.FirstOrDefault(i => i.Id == poli.insurance_id);
            ViewBag.InsuranceName = insu?.Name ?? "";
            return View(poli);
        }
    }
}
