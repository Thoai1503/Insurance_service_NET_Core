using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class ContractController : Controller
    {
        // GET: ContractController
        public ActionResult Index()
        {
            var user = HttpContext.Session.GetObject<User>("user");
            var contracts = ContractRepository.Instance.GetAll();
            var unassignedContracts = contracts.Where(c => c.employee_id == 0).OrderByDescending(c => c.StartDate).ToHashSet();
            var assignedContracts = contracts.Where(c => c.employee_id != 0).ToHashSet();
            ViewBag.user = user;
            var employees = UserRepository.Instance.GetAllEmployeeUser();
            ViewBag.UnassignedContracts = unassignedContracts;
            ViewBag.AssignedContracts = assignedContracts;
            ViewBag.Employees = employees;
            return View(contracts);
        }

        // GET: ContractController/Details/5

        // GET: ContractController/Create
        public ActionResult Create()
        {
            var user = HttpContext.Session.GetObject<User>("user");
            ViewBag.user = user;
            return View();
        }

        // POST: ContractController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            var user = HttpContext.Session.GetObject<User>("user");
            ViewBag.user = user;
            try
            {

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContractController/Edit/5
        public ActionResult Detail(int id)
        {
            var user = HttpContext.Session.GetObject<User>("user");
            ViewBag.user = user;
            var contract = ContractRepository.Instance.FindById(id);
            return View(contract);
        }

        // POST: ContractController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var user = HttpContext.Session.GetObject<User>("user");
                ViewBag.user = user;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContractController/Delete/5
        public ActionResult Delete(int id)
        {
            var user = HttpContext.Session.GetObject<User>("user");
            ViewBag.user = user;
            return View();
        }

        // POST: ContractController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
