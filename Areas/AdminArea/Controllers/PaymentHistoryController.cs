using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class PaymentHistoryController : Controller
    {
        public IActionResult Index(int month = 0,int page =1,int pageSize=10)
        {
            var paymentHistory = new HashSet<PaymentHistory>();
            paymentHistory = PaymentRepository.Instance.GetAll(month);
            ViewBag.payment = PaymentRepository.Instance.Paging(month,page,pageSize);
            ViewBag.page = page;
            ViewBag.allpayment = paymentHistory;
            ViewBag.month = month;
            return View();
        }
    }
}
