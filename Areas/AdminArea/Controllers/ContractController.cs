﻿using Insurance_agency.Models.Repository;
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
            var unassignedContracts = contracts.Where(c=> c.employee_id ==0).OrderByDescending(c=>c.StartDate).ToHashSet();
            var assignedContracts = contracts.Where(c => c.employee_id != 0).ToHashSet();
            ViewBag.UnassignedContracts = unassignedContracts;
            ViewBag.AssignedContracts = assignedContracts;

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
    }
}
