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
                var insurance = _context.Insurances
                .Where(i => i.Id == p.insurance_id).Select(i => i.Name)
                .FirstOrDefault() ?? "";
                return new
                {
                    p.id,
                    p.name,
                    p.description,
                    p.age_max,
                    p.age_min,
                    p.active,
                    insurance_name = insurance
                };
            }).ToList();
            ViewBag.Policies = poliWithInsurance;
            ViewBag.Insurance = _context.Insurances.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create([FromBody] Policy en)
        {

            var rs = PolicyRepository.Instance.Create(en);
            return Json(rs);
        }
   
        public IActionResult Delete(int id)
        {
            var rs = PolicyRepository.Instance.Delete(id);
            return Json(rs);
        }
        public IActionResult Details(int id)
        {
            var poli = PolicyRepository.Instance.FindById(id);
            if (poli == null) return NotFound();
            var insu = _context.Insurances.FirstOrDefault(i => i.Id == poli.insurance_id);
            ViewBag.InsuranceName = insu?.Name ?? "";
            return View(poli);
        }
        [HttpPost]
        public IActionResult Update([FromBody] Policy en)
        {
            var poli = PolicyRepository.Instance.Update(en);
            return Json(poli);
        }
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var poli = PolicyRepository.Instance.FindById(id);
            if (poli == null) return NotFound();
            return Json(poli);
        }
    }
}
