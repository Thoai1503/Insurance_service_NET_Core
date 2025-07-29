using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class PaymentHistoryController : Controller
    {
        public IActionResult Index(int month = 0,int page =1,int pageSize=10,int client=0)
        {
            var paymentHistory = new HashSet<PaymentHistory>();
            var client1 = UserRepository.Instance.GetCustomerUser();
            var employee = UserRepository.Instance.GetAllEmployeeUser();
            paymentHistory = PaymentRepository.Instance.GetAll(month,client);
            ViewBag.payment = PaymentRepository.Instance.Paging(month,page,pageSize,client);
            ViewBag.clientId = client;
            ViewBag.client = client1;
            ViewBag.employee = employee;
            ViewBag.page = page;
            ViewBag.allpayment = paymentHistory;
            ViewBag.month = month;
            return View();
        }
    }
}
