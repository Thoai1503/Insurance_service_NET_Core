using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class LoanController : Controller
    {
        SendMailController sendMailController;
        public LoanController()
        {
            // Constructor logic if needed
            sendMailController = new SendMailController();
        }
        public IActionResult Index()
        {
            var user = HttpContext.Session.GetObject<User>("user");
            var loanList = new HashSet<Loan>();
            if (user.auth_id!=1)
            {
                loanList = LoanRepository.Instance.GetAllByEmployeeId(user.id);



            }else
            {
                // If the user is an admin, get all loans
                loanList = LoanRepository.Instance.GetAll();
            }
       
            return View(loanList);
        }
        public IActionResult ApproveLoan(int id)
        {
            var user = HttpContext.Session.GetObject<User>("user");
            var loan = LoanRepository.Instance.FindById(id);
            if (loan != null)
            {
                loan.Status = 1; // Assuming 1 is the status for approved loans

            }

            // Approve the loan with the given ID
            var result = LoanRepository.Instance.Update(loan);
            if (!result)
            {
                // Handle the case where the update failed
            return Json(new { success = false, message = "Failed to approve the loan." });
            }
            else
            {
                sendMailController.Index(user.id, loan.Contract.user.email, "Loan Approved", $"Your loan with ID {loan.Id} has been approved. The money will be transferred to your account in the next 2-3 days.",(int)loan.Contract.user_id);
                // Redirect to the index action after successful approval
                return Json(new { success = true, message = "Loan approved successfully." });
            }

        }
        public IActionResult RejectLoan(int id)
        {
            var user = HttpContext.Session.GetObject<User>("user");
            var loan = LoanRepository.Instance.FindById(id);
            if (loan != null)
            {
                loan.Status = 2; // Assuming 2 is the status for rejected loans

            }

            // Approve the loan with the given ID
            var result = LoanRepository.Instance.Update(loan);
            if (!result)
            {
                // Handle the case where the update failed
                return Json(new { success = false, message = "Failed to approve the loan." });
            }
            else
            {
                sendMailController.Index(user.id, loan.Contract.user.email, "Loan Rejected", $"Your loan with ID {loan.Id} has been rejected.", (int)loan.Contract.user_id);
                // Redirect to the index action after successful approval
                return Json(new { success = true, message = "Loan approved successfully." });
            }

        }
    }
}
