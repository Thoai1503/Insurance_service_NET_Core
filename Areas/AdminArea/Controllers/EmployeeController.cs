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
        public IActionResult Create(IFormFile Img, User usr)
        {
            if (usr != null)
            {
                if (Img != null && Img.Length > 0)
                {
                    var fileName = Path.GetFileName(Img.FileName);
                    // Ensure the file name is unique to avoid conflicts
                    fileName = $"{Guid.NewGuid()}_{fileName}";

                    var filePath = Path.Combine("wwwroot/Content/Admin/img/avatar", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Img.CopyTo(stream);
                    }
                    usr.avatar = fileName;
                }
                else
                {
                    usr.avatar = "user.jpg";
                }
                var result = UserRepository.Instance.CreateEmployee(usr);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Failed to create employee.";

                }
            }

            return View();
        }
        public IActionResult Edit(User user, IFormFile Img)
        {
            if (user != null)
            {
                if (Img != null && Img.Length > 0)
                {
                    var fileName = Path.GetFileName(Img.FileName);
                    var filePath = Path.Combine("wwwroot/Content/Admin/img/avatar", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Img.CopyTo(stream);
                    }
                    user.avatar = fileName;
                }
                else
                {
                    user.avatar = "user.jpg";
                }
                var result = UserRepository.Instance.Update1(user);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Failed to update employee.";
                }
            }


            return View();
        }
        public IActionResult UpdateAvatar(int id, IFormFile Img)
        {
            if (Img != null && Img.Length > 0)
            {
                var fileName = Path.GetFileName(Img.FileName);
                // Ensure the file name is unique to avoid conflicts
                fileName = $"{Guid.NewGuid()}_{fileName}";
                var filePath = Path.Combine("wwwroot/Content/Admin/img/avatar", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Img.CopyTo(stream);
                }
                var user = UserRepository.Instance.FindById(id);
                user.avatar = fileName;
                var re = UserRepository.Instance.Update(user);
                if (re)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Failed to update avatar.";


                }
            }
            else
            {
                ViewBag.Error = "No image file provided.";
            }
            return RedirectToAction("Index");
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
