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
        public async Task< ActionResult> Index(string? search)
        {
            var contracts = await ContractRepository.Instance.GetAll();
            var unassignedContracts = contracts.Where(c=> c.employee_id ==0).OrderByDescending(c=>c.StartDate).ToHashSet();
            var assignedContracts = contracts.Where(c => c.employee_id != 0).ToHashSet();
            var employees = UserRepository.Instance.GetAllEmployeeUser();
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {

                if (!string.IsNullOrEmpty(search))
                {
                    contracts = ContractRepository.Instance.FindByKeywork(search);
                }
                return Json(new { message = "AJAX request", contracts });
            }

            ViewBag.UnassignedContracts = unassignedContracts;
            ViewBag.AssignedContracts = assignedContracts;
            ViewBag.Employees = employees;
            return View(contracts);
        }

        // GET: ContractController/Details/5
   
        // GET: ContractController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContractController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
        public IActionResult UpdateEmployeeToContract(int employee_id, int contract_id, int current_employee)
        {
            var contract = ContractRepository.Instance.FindById(contract_id);
            if (contract != null)
            {
                // Check if the contract is already assigned to an employee
                if (current_employee != 0)
                {
                    contract.employee_id = employee_id;

                    // Update the employee_id in the contract
                    var result = ContractRepository.Instance.Update(contract);
                    var unCompletedHistory = AssignHistoryRepository.Instance.FindUnCompletedHistory(contract_id,current_employee);
                    if (unCompletedHistory != null)
                    {
                        // Update the end date of the uncompleted history
                        unCompletedHistory.end_date = DateTime.Now;
                        var updateHistoryResult = AssignHistoryRepository.Instance.Update(unCompletedHistory);
                        if (!updateHistoryResult)
                        {
                            ViewBag.Error = "Failed to update assignment history.";
                            return RedirectToAction("Index", "Contract", new { id = employee_id });
                        }
                    }

                    if (result && unCompletedHistory!=null)
                    {
                        // Create an AssignHistoryView record
                        var assignHistory = new AssignHistoryView
                        {
                            employee_id = employee_id,
                            contract_id = contract_id,
                            start_date = DateTime.Now,
                            end_date = null // Assuming end date is start date + number of years paid
                        };
                        // Save the assign history
                        var assignHistoryResult = AssignHistoryRepository.Instance.Create(assignHistory);
                        if (!assignHistoryResult)
                        {
                            ViewBag.Error = "Failed to create assignment history.";
                            return RedirectToAction("Index", "Contract", new { id = employee_id });
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Failed to update contract.";
                        return RedirectToAction("Index", "Contract", new { id = employee_id });
                    }

                }

                // If the contract is not assigned to any employee, assign it to the new employee
                else
                {
                    contract.employee_id = employee_id;
                    var result = ContractRepository.Instance.Update(contract);
                    if (result)
                    {
                        // Create an AssignHistoryView record
                        var assignHistory = new AssignHistoryView
                        {
                            employee_id = employee_id,
                            contract_id = contract_id,
                            start_date = DateTime.Now,
                            end_date = null // Assuming end date is start date + number of years paid
                        };
                        // Save the assign history
                        var assignHistoryResult = AssignHistoryRepository.Instance.Create(assignHistory);
                        if (!assignHistoryResult)
                        {
                            ViewBag.Error = "Failed to create assignment history.";
                            return RedirectToAction("Index", "Contract", new { id = employee_id });
                        }
                    }
                    
                    else
                    {
                        ViewBag.Error = "Failed to update contract.";
                        return RedirectToAction("Index", "Contract", new { id = employee_id });
                    }
                }

            }
            else
            {
                ViewBag.Error = "Contract not found.";
            }
            return RedirectToAction("Index");
        }
    }
}
