using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            var customers = UserRepository.Instance.GetCustomerUser();
            return View(customers);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Detail(int id)
        {
            var client = UserRepository.Instance.FindById(id);
            var contract = ContractRepository.Instance.GetContractsByUserId(id);
            ViewBag.client = client;
            ViewBag.contract = contract;
            return View(client);
        }
        public IActionResult Edit(User customer)
        {
            if (customer != null)
            {
                UserRepository.Instance.Update(customer);
            }
            return RedirectToAction("Index");
        }
        public IActionResult updateStatus(User user)
        {
            if (user != null)
            {
                if (UserRepository.Instance.ChangeStatus(user))
                {
                    return Json(user);
                }
            }
            return Json(user);
        }
    }
}
