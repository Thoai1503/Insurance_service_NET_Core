using Azure.Core;
using Insurance_agency.Models.Entities;
using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class InsuranceManagementController : Controller
    {

        public async Task<IActionResult> Index(int page = 1, int pageSize = 6, int insurance_type_id = -1, int sort_by = -1)
        {
            var insurancetype = await InsuranceTypeRepository.Instance.GetAll();
            var item = InsuranceRepository.Instance.GetAll();
            var user = HttpContext.Session.GetObject<User>("user");
            ViewBag.user=user;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var insList = new HashSet<InsuranceView>();
                var search = Request.Query["keyword"];
                if (search.Count > 0)
                {
                    insList = InsuranceRepository.Instance.FindByKeywork(search);
                }
                return Json(new { message = "AJAX request được xử lý", insList });
            }

            if (insurance_type_id > 0)
            {
                item = item.Where(d => d.insurance_type_id == insurance_type_id).ToHashSet();
            }
            if (sort_by == 1)
            {
                item = item.OrderBy(d => d.name).ToHashSet();
            }
            else if (sort_by == 2)
            {
                item = item.OrderByDescending(d => d.name).ToHashSet();
            }
            //else if (sort_by == 3)
            //{
            //    item = item.OrderBy(d => d.value).ToHashSet();
            //}
            //else if (sort_by == 4)
            //{
            //    item = item.OrderByDescending(d => d.value).ToHashSet();
            //}
            //else if (sort_by == 5)
            //{
            //    item = item.OrderBy(d => d.year_max).ToHashSet();
            //}
            //else if (sort_by == 6)
            //{
            //    item = item.OrderByDescending(d => d.year_max).ToHashSet();
            //}
            //Create pagination for item

            if (Request.Query.ContainsKey("page"))
            {
                int.TryParse(Request.Query["page"], out page);
            }
            int totalItems = item.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            // Get the items for the current page
            int skip = (page - 1) * pageSize;
            ViewBag.item = item.Skip(skip).Take(pageSize).ToHashSet();





            return View(insurancetype);
        }
        public ActionResult Create()
        {
            var insurancetype = InsuranceTypeRepository.Instance.GetAll();
            ViewBag.insurancetype = insurancetype;
            return View();
        }
        [HttpPost]
        public ActionResult CreateConfirm(IFormFile Img, InsuranceView item)
        {
            try
            {
                if (item != null && Img != null)
                {
                    string folder = "Image/Ex/";
                    string name = Img.FileName;
                    name = name.Replace("-", "");
                    name = name.Replace(" ", "");
                    name = name.Replace("_", "");
                    folder += name;
                    string fullPathSave = Path.Combine("wwwroot/Content", folder);
                    item.ex_image = name;
                    item.description = item.description ?? string.Empty;
                    using (var fileStream = new FileStream(fullPathSave, FileMode.Create))
                    {
                        Img.CopyTo(fileStream);
                    }


                }
                if (InsuranceRepository.Instance.Create(item))
                {
                    return RedirectToAction("Index");
                }
                ;

            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }
        public ActionResult InsuranceDetail(int id)
        {
            var item = PolicyRepository.Instance.GetAllByInsuranceId(id);
            var insurance = InsuranceRepository.Instance.FindById(id);
            var type = InsuranceTypeRepository.Instance.FindById(insurance.insurance_type_id);
            //  ViewBag.item = item;
            ViewBag.insuranceId = id;
            ViewBag.type = type;
            ViewBag.insurance = insurance;
            return View(item);


        }
        public async Task<ActionResult> InsuranceEdit(int id)
        {
            var insurancetype = await InsuranceTypeRepository.Instance.GetAll();
            ViewBag.insurancetype = insurancetype;
            ViewBag.data = InsuranceRepository.Instance.FindById(id);
            return View();
        }

        public ActionResult EditConfirm(IFormFile Img, InsuranceView item)
        {
            var data = InsuranceRepository.Instance.FindById(item.id);
            try
            {
                if (item != null && Img != null)
                {
                    string OldImg = data.ex_image;
                    string folder = "Image/Ex/";
                    string name = Img.FileName;
                    name = name.Replace("-", "");
                    name = name.Replace(" ", "");
                    name = name.Replace("_", "");
                    string fullPathSave = Path.Combine("wwwroot/Content", folder + name);
                    string oldPathSave = string.Empty;
                    if (OldImg != null && OldImg != string.Empty)
                    {
                        oldPathSave = Path.Combine("wwwroot/Content", folder + OldImg);
                    }
                    item.ex_image = name;
                    item.description = item.description ?? string.Empty;
                    if (!Path.Exists(fullPathSave))
                    {
                        if (Img.FileName != OldImg)
                        {
                            if (oldPathSave != string.Empty)
                            {
                                System.IO.File.Delete(oldPathSave);
                            }
                            using (var fileStream = new FileStream(fullPathSave, FileMode.Create))
                            {
                                Img.CopyTo(fileStream);
                            }
                        }
                    }


                }
                if (InsuranceRepository.Instance.Update(item))
                {
                    return RedirectToAction("Index");
                }
                ;

            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id = 0)
        {
            try
            {
                if (id > 0)
                {
                    var item = InsuranceRepository.Instance.FindById(id);
                    if (InsuranceRepository.Instance.Delete(id))
                    {
                        string folder = "Image/Ex/";
                        string fullPathSave = Path.Combine("wwwroot/Content", folder + item.ex_image);
                        System.IO.File.Delete(fullPathSave);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }
        public ActionResult UpdateActiveStatus(int _id, int _checked)
        {


            var result = InsuranceRepository.Instance.Active(_id, _checked);

            return Json(new { success = result });
        }

    }
}
