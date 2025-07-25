using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Controllers
{
    public class LoanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RequestForm(int contractId)
        {
            var contract = ContractRepository.Instance.FindById(contractId);
            return View(contract);
        }
        public IActionResult MakeRequest(int contractId, int amount)
        {
            var contract = ContractRepository.Instance.FindById(contractId);
            if (contract == null)
            {
                return NotFound();
            }
            // Assuming you have a method to create a loan request
            var loanRequest = new Loan
            {
                ContractId = contractId,
                LoanAmount = amount,

                RequestDate = DateTime.Now,
                Status = 0 // Assuming 0 means pending

            };
            var result = LoanRepository.Instance.Create(loanRequest);
            if (!result) 
            {
                // Handle the case where the loan request could not be created
                return BadRequest("Could not create loan request.");
            }else
            {
                return RedirectToAction("Index","UserProfile");
            }
            // Save the loan request to the database (not shown here)
            // LoanRepository.Instance.Add(loanRequest);
          
        }

    }
}
