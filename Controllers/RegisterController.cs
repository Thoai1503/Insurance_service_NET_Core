using Insurance_agency.Models;
using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Mail;
using System.Net;

namespace Insurance_agency.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.SetInt32("allbanner", 0);
            return View();
        }

        public void sendmail(string email)
        {
            string to = email;
            string code = Function.GenerateCode();
            HttpContext.Session.SetObject("code", code);
            string from = "minhphat1612@gmail.com";
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Verification Code";
            message.Body = @"" + code;
            try
            {
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    NetworkCredential NetworkCred = new NetworkCredential(from, "xhhe ikpv aazx osjq");
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                    ex.ToString());
            }
        }
        public IActionResult Register(IFormFile Img, User item, int code = 0)
        {
            try
            {
                if (code != 0)
                {
                    if (item != null)
                    {
                        item.active = 1;
                        if (int.TryParse(HttpContext.Session.GetObject<string>("code"), out int number))
                        {
                            if (number == code)
                            {
                                if (Img != null)
                                {
                                    string folder = "Image/Avatar/Customer/";
                                    string name = Img.FileName;
                                    name = name.Replace("-", "");
                                    name = name.Replace(" ", "");
                                    name = name.Replace("_", "");
                                    folder += name;
                                    string fullPathSave = Path.Combine("wwwroot/Content", folder);
                                    item.avatar = name;
                                    using (var fileStream = new FileStream(fullPathSave, FileMode.Create))
                                    {
                                        Img.CopyTo(fileStream);
                                    }
                                }
                                else
                                {
                                    item.avatar = "no avatar";
                                }
                                item.password = Function.MD5Hash(item.password);
                                var a = UserRepository.Instance.Create(item);
                                if (a == false)
                                {
                                    return RedirectToAction("Create");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
            return RedirectToAction("Index", "Login");
        }
    }
}
