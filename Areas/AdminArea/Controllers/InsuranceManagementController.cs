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
                    item.ExImage = name;
                    using (var fileStream = new FileStream(fullPathSave, FileMode.Create))
                    {
                        Img.CopyTo(fileStream);
                    }
                    
                    if (InsuranceRepository.Instance.Create(item))
                    {
                        return RedirectToAction("Index");
                    };
                }
                
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("Index");
        }
    }
}
