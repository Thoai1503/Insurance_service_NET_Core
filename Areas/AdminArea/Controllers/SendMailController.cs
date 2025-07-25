using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Insurance_agency.Services.SendMail;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SendMailController : Controller
    {
        public async Task<ActionResult> Index(int from,string email,string title, string content, int to=0)
        {
            if(to != 0)
            {
                var emailService = new EmailService();
                await emailService.SendEmailAsync(email, title, content);
                Notification notification = new Notification
                {
                    detail = content,
                    notification_type_id = 0, // Assuming 1 is the ID for email notifications
                    status = 0, // Assuming 0 means pending or new

                   
                    from = (int)from, // Assuming 'from' is the sender's email
                    to = to, // Assuming 'to' is the recipient's email
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
                await emailService.SendEmailAsync(email, title, content);

                return Json(new { message = "Email sent successfully!" });
            }

            return Json(new { message = "Email sent successfully!" });
            
        }
    }
}
