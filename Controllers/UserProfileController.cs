using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;

namespace Insurance_agency.Controllers
{
    public class UserProfileController : Controller
    {
        public IActionResult Index()
        {
            var user = HttpContext.Session.GetObject<User>("user");
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var notificationCount = NotificationRepository.Instance.GetUnreadNotificationsByUserId(user.id)
                .Where(n => n.is_read == 0).Count();
            HttpContext.Session.SetInt32("allbanner", 0);
            ViewBag.NotificationCount = notificationCount;
            return View(user);
        }
        public IActionResult ContractHistory()
        {

            var user = HttpContext.Session.GetObject<User>("user");
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var totalAmount = 0;
            var contracts = ContractRepository.Instance.GetContractsByUserId(user.id);
            var totalPaid = contracts.Sum(c => c.total_paid);
            var contrNum = contracts.Count();
            DateTime nextDue = contracts.Min(c => c.next_payment_due);
            var displayDate = nextDue.ToString("dd MMM yyyy");
            ContractView nextDueContract = contracts.Where(c => c.next_payment_due == nextDue).FirstOrDefault();
            var notificationCount = NotificationRepository.Instance.GetUnreadNotificationsByUserId(user.id)
    .Where(n => n.is_read == 0).Count();

            ViewBag.NotificationCount = notificationCount;
            ViewBag.TotalPaid = totalPaid;
            ViewBag.Contracts = contracts;
            ViewBag.Count = contrNum;
            ViewBag.NextDue = displayDate;
            ViewBag.NextDueContract = nextDueContract;
            HttpContext.Session.SetInt32("allbanner", 0);
            return View(contracts);
        }
        public IActionResult PaymentHistory(int contractId)
        {
            var user = HttpContext.Session.GetObject<User>("user");
            var payment = PaymentRepository.Instance.FindByContractId(contractId).OrderByDescending(c => c.id).ToHashSet();
            var paymentCount = payment.Count;


            var contract = ContractRepository.Instance.FindById(contractId);

            var remainPayment = contract.number_year_paid - paymentCount;
            //Estimate the next payment year

            string nextDueDate = contract.StartDate.AddYears(paymentCount).ToString();

            DateTime date = DateTime.ParseExact(nextDueDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            // Set time to midnight
            var displayDate = date.ToString("dd MMM yyyy");
            var password = BCrypt.Net.BCrypt.HashPassword("Thoaivip13");

            var notificationCount = NotificationRepository.Instance.GetUnreadNotificationsByUserId(user.id)
    .Where(n => n.is_read == 0).Count();
            HttpContext.Session.SetInt32("allbanner", 0);
            ViewBag.NotificationCount = notificationCount;
            ViewBag.NextDueDate = displayDate;
            ViewBag.Id = contractId;
            ViewBag.RemainTime = remainPayment;
            ViewBag.Contract = contract;
            return View(payment);
        }
        public IActionResult Notification()
        {
            var user = HttpContext.Session.GetObject<User>("user");
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var notifications = NotificationRepository.Instance.GetAllNotificationsByUserId(user.id).OrderByDescending(c=>c.id).ToHashSet();
            var notificationCount = NotificationRepository.Instance.GetUnreadNotificationsByUserId(user.id)
    .Where(n => n.is_read == 0).Count();
            HttpContext.Session.SetInt32("allbanner", 0);
            ViewBag.NotificationCount = notificationCount;
            ViewBag.Notifications = notifications;
            HttpContext.Session.SetInt32("allbanner", 0);
            return View(notifications);
        }
        public IActionResult LogOut()
        {
            try
            {
                if (HttpContext.Session.GetObject<User>("user") != null)
                {
                    HttpContext.Session.Remove("user");
                }

            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("index", "Login");
        }
        public IActionResult ContractInfo(int contractId = 0)
        {
            try
            {
                if (HttpContext.Session.GetObject<User>("user") != null)
                {
                    if (contractId == 0)
                    {
                        return RedirectToAction("index");
                    }
                    var user = HttpContext.Session.GetObject<User>("user");
                    ViewBag.user = user;

                    var contract = ContractRepository.Instance.getById(contractId);
                    ViewBag.insurance = InsuranceRepository.Instance.FindById((int)contract.insurance_id);
                    ViewBag.contract = contract;
                }
                else
                {
                    return RedirectToAction("index", "Home");
                }
                HttpContext.Session.SetInt32("allbanner", 0);
            }
            catch (Exception ex)
            {
            }
            return View();
        }
        public IActionResult updateProfile(User user)
        {
            try
            {
                if (user != null)
                {
                   var a= UserRepository.Instance.Update1(user);
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("index");
        }
        public IActionResult isreadupdate(int notificationId)
        {
           
               //     var notiId = Request.Query["notificationId"];
            var notification = NotificationRepository.Instance.FindById(notificationId);
                    if (notification != null )
                    {
                        notification.is_read = 1;
                     var result=   NotificationRepository.Instance.UpdateNotification(notification);

                return Json(new { success = result, message = "Notification updated successfully." });
            }
                    return Json(new { success = false, message = "Notification not found." });

        }
    }
}
