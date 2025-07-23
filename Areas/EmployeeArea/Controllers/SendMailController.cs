using Insurance_agency.Services.SendMail;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Areas.EmployeeArea.Controllers
{
    [Area("EmployeeArea")]
    public class SendMailController : Controller
    {
        public async Task<ActionResult> Index(string to,string title, string content)
        {
            var emailService = new EmailService();
            await emailService.SendEmailAsync("vothoai1503@gmail.com", title, content);
            return Json(new { message = "Email sent successfully!" });
            
        }
    }
}
