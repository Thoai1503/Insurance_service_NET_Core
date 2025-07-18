using System.Text.RegularExpressions;
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
        public IActionResult Index(string? search,bool searchInsurance = false, int page = 1, int pageSize = 10)
        {
            var pageRes = PolicyRepository.Instance.PaginationSearch(search,searchInsurance,page,pageSize);
            var poliWithInsurance = pageRes.Items.Select(p =>
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
            ViewBag.TotalItem = pageRes.TotalItem;
            ViewBag.PageNumber = pageRes.PageNumber;
            ViewBag.PageSize = pageRes.PageSize;
            ViewBag.Search = search;
            ViewBag.SearchInsurance = searchInsurance;
            ViewBag.Insurance = _context.Insurances.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Policy en)
        {
            if (string.IsNullOrWhiteSpace(en.name))
                return Json(new { success = false, message = "Policy name can't be blank" });
            if (string.IsNullOrEmpty(en.description)) return Json(new { success = false, message = "Description can't be blank" });
            if (en.age_min == 0 || en.age_max == 0) return Json(new { success = false, message = "Age can't be blank" });
            var nameRegex = new Regex(@"^[a-zA-Z0-9\s_-]+$");
            if (!nameRegex.IsMatch(en.name))
                return Json(new { success = false, message = "Plicy name can't contain special characters" });
            if (en.age_min < 0 || en.age_max < 0)
                return Json(new { success = false, message = "Age isn't negative" });
            if (en.age_max < en.age_min)
                return Json(new { success = false, message = "maximum age mustn't be less than minimum age" });
            if (en.age_min == en.age_max)
                return Json(new { success = false, message = "minimum age can't be equal to maximum age" });
            var duplicate = PolicyRepository.Instance.GetAll()
                .Any(p => p.id != en.id && (p.name.Equals(en.name, StringComparison.OrdinalIgnoreCase)
                || p.description.Equals(en.description, StringComparison.OrdinalIgnoreCase)));
            if (duplicate) return Json(new { success = false, message = "policy name or description already exitst" });
            var rs = PolicyRepository.Instance.Create(en);
            return Json(new { success = rs, message = rs ? "Create success" : "Create Fail" });
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
        public IActionResult Update(Policy en)
        {
            if (string.IsNullOrWhiteSpace(en.name))
                return Json(new { success = false, message = "Policy name can't be blank" });
            if (string.IsNullOrEmpty(en.description)) return Json(new { success = false, message = "Description can't be blank" });
            if (en.age_min == 0 || en.age_max == 0) return Json(new { success = false, message = "Age can't be blank" });
            var nameRegex = new Regex(@"^[a-zA-Z0-9\s_-]+$");
            if (!nameRegex.IsMatch(en.name))
                return Json(new { success = false, message = "Plicy name can't contain special characters" });
            if (en.age_min < 0 || en.age_max < 0)
                return Json(new { success = false, message = "Age isn't negative" });
            if (en.age_max < en.age_min)
                return Json(new { success = false, message = "maximum age mustn't be less than minimum age" });
            if (en.age_min == en.age_max)
                return Json(new { success = false, message = "minimum age can't be equal to maximum age" });
            var duplicate = PolicyRepository.Instance.GetAll()
                .Any(p => p.id != en.id && (p.name.Equals(en.name, StringComparison.OrdinalIgnoreCase)
                || p.description.Equals(en.description, StringComparison.OrdinalIgnoreCase)));
            if (duplicate) return Json(new { success = false, message = "policy name or description already exitst" });
            var poli = PolicyRepository.Instance.Update(en);
            return Json(new { success = poli, meseage = poli ? "Edit success" : "Edit Fail" });
        }
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var poli = PolicyRepository.Instance.FindById(id);
            if (poli == null) return NotFound();
            return Json(poli);
        }
        [HttpPost]
        public JsonResult ToggleActive([FromBody] Policy po)
        {
            try
            {
                var res = PolicyRepository.Instance.ToggleActive(po.id, po.active);
                return Json(new { success = res });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
