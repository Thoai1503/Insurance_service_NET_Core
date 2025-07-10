using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Controllers
{
    public class UserProfileController : Controller
    {
        public IActionResult Index(int id = 1)
        {
            HttpContext.Session.SetInt32("allbanner", 0);
            ViewBag.data = PaymentHistoryRepository.Instance.GetByUserId(id);
            return View();
        }
        public IActionResult PaymentHistory(int id = 0)
        {
            try
            {
                ViewBag.data = PaymentHistoryRepository.Instance.GetByUserId(id);

            }
            catch (Exception ex)
            {
            }
            return View();
        }
        public IActionResult DemoPayment(int id)
        {
            var item = ContractRepository.Instance.FindById(id);
            ViewBag.data = item;
            return View();
        }
        public IActionResult DemoPaymentConfirm(PaymentHistory history)
        {
            if (history != null)
            {
                history.Contract= ContractRepository.Instance.FindById(history.contract_id); 
                var contract = history.Contract;
                contract.total_paid += history.amount;
                ContractRepository.Instance.Update(contract);
                PaymentHistoryRepository.Instance.Create(history);
            }
            return RedirectToAction("Index");
        }
    }
}
