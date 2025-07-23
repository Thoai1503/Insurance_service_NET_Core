using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Insurance_agency.Services.SendMail;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Areas.EmployeeArea.Controllers
{
    [Area("EmployeeArea")]
    public class SendMailController : Controller
    {
        public async Task<ActionResult> Index(string to,string title, string content, int userId =0)
        {
            if(userId != 0)
            {
                var emailService = new EmailService();
                await emailService.SendEmailAsync("vothoai1503@gmail.com", title, content);
                Notification notification = new Notification
                {
                    detail = content,
                    notification_type_id = 0, // Assuming 1 is the ID for email notifications
                    status = 0, // Assuming 0 means pending or new
                    user_id = userId,
                    is_read = 0 // Assuming 0 means unread
                };
                var result = NotificationRepository.Instance.Create(notification);
                if (!result)
                {
                    return Json(new { message = "Failed to send email and create notification.",success =result });
                }
                return Json(new { message = "Email sent and notification created successfully!",success= result });
            }
            else
            {
                var emailService = new EmailService();
                await emailService.SendEmailAsync(to, title, content);

                return Json(new { message = "Email sent successfully!" });
            }

            return Json(new { message = "Email sent successfully!" });
            
        }
    }
}
