using Insurance_agency.Services.SendMail;
using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Insurance_agency.Models.ModelView;

namespace Insurance_agency.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class DashboardController : Controller
    {
        // GET: DashboardController
        public async Task< ActionResult> Index()
        {
            ViewBag.data = await ContractRepository.Instance.GetAll();
            ViewBag.customer = UserRepository.Instance.GetCustomerUser();
            ViewBag.unassign = ContractRepository.Instance.GetInassignContracts().Count;
            List<long> earning = new List<long>();
            for (int i = 5; i >= 0; i--)
            {
                int Date = DateTime.Now.Month;
                earning.Add(PaymentRepository.Instance.GetEarning(Date-i));
            }
            ViewBag.monthearn = PaymentRepository.Instance.GetEarning(DateTime.Now.Month);
            ViewBag.earning = earning;
            return View();
        }

        public ActionResult TextEditor()
        {
            return View();
        }

        // GET: DashboardController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DashboardController/Create
        public ActionResult Chart()
        {
            return View();
        }

        // POST: DashboardController/Create
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

        // GET: DashboardController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DashboardController/Edit/5
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

        // GET: DashboardController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DashboardController/Delete/5
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
        public async Task<ActionResult> SendMail()
        {
            var emailService = new EmailService();
            await emailService.SendEmailAsync("user@gmail.com", "Test", "Hello from Gmail SMTP");
            return Json(new { message = "Email sent successfully!" });
        }
    }
}
