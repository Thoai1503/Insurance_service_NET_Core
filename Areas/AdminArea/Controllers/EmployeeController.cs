using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            var employees = UserRepository.Instance.GetAllEmployeeUser();
            return View(employees);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult CreateConfirm(IFormFile Img, User item)
        {
            if (item != null)
            {
                if (Img != null)
                {
                    string folder = "Image/Avatar/Employee/";
                    string name = Img.FileName;
                    name = name.Replace("-", "");
                    name = name.Replace(" ", "");
                    name = name.Replace("_", "");
                    folder += name;
                    string fullPathSave = Path.Combine("wwwroot/Content", folder);
                    item.avatar = name;
                    using (var fileStream = new FileStream(fullPathSave, FileMode.Create))
                    {
                        Img.CopyTo(fileStream);
                    }
                }
                else
                {
                    item.avatar = "no avatar";
                }
                var a = UserRepository.Instance.CreateEmployee(item);
                if (a == false)
                {
                    return RedirectToAction("Create");
                }
            }
            return RedirectToAction("index");
        }
            
        public IActionResult EmployeeDetail(int id)
        {
            var employee = UserRepository.Instance.FindById(id);
            var contracts = ContractRepository.Instance.FindByEmployeeId(id);
            ViewBag.Contracts = contracts;
            return View(employee);
        }
    }
}
