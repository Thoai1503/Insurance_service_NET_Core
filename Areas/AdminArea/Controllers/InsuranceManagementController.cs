using Insurance_agency.Models.Entities;
using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class InsuranceManagementController : Controller
    {
        
        public IActionResult Index()
        {
            var item = InsuranceRepository.Instance.GetAll();
            ViewBag.item = item;
          
            return View();
        }
        public ActionResult Create()
        {
            var insurancetype = InsuranceTypeRepository.Instance.GetAll();
            ViewBag.insurancetype = insurancetype;
            return View();
        }
        [HttpPost]
        public ActionResult CreateConfirm(IFormFile Img,InsuranceView item)
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
                    string fullPathSave = Path.Combine("wwwroot/Content",folder);
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
                };

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
        public ActionResult InsuranceEdit(int id)
        {
            var insurancetype = InsuranceTypeRepository.Instance.GetAll();
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
                    string OldImg =data.ex_image;
                    string folder = "Image/Ex/";
                    string name = Img.FileName;
                    name = name.Replace("-", "");
                    name = name.Replace(" ", "");
                    name = name.Replace("_", "");
                    string fullPathSave = Path.Combine("wwwroot/Content", folder+name);
                    string oldPathSave = string.Empty;
                    if (OldImg != null && OldImg != string.Empty)
                    {
                       oldPathSave= Path.Combine("wwwroot/Content", folder + OldImg);
                    }
                    item.ex_image = name;
                    item.description = item.description ?? string.Empty;
                    if (!Path.Exists(fullPathSave)) 
                    {
                        if (Img.FileName != OldImg)
                        {
                            if (oldPathSave!=string.Empty)
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
                };

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
    }
}
