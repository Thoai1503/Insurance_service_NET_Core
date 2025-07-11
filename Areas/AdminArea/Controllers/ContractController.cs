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
            var contracts = ContractRepository.Instance.GetAll();

            return View(contracts);
        }

        // GET: ContractController/Details/5
   
        // GET: ContractController/Create
        public ActionResult Create(int employeeId=1)
        {
            try
            {
                ViewBag.customer = UserRepository.Instance.GetCustomerUser();
                ViewBag.data = InsuranceRepository.Instance.GetAll();
                ViewBag.id = employeeId;
            }
            catch (Exception ex)
            {
            }
            return View();

        }

        // POST: ContractController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateConfirm(ContractView item)
        {
            try
            {
                if (item != null)
                {
                    DateTime date = (DateTime)item.StartDate;
                    item.EndDate = date.AddYears((int)item.number_year_paid);
                    item.status = 1;
                    ContractRepository.Instance.Create(item);
                }

            }
            catch
            {
                return View();
            }
            return RedirectToAction("index");
        }

        // GET: ContractController/Edit/5
        public ActionResult Detail(int id)
        {
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
