using Insurance_agency.Models;
using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using System.Net.Mail;

using Insurance_agency.Helper;
using Azure;

namespace Insurance_agency.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Register";
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
        public bool checkEmail(string email)
        {
            try
            {
                if (UserRepository.Instance.checkEmail(email))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
            }
            return true;
        }
        public IActionResult Register(IFormFile Img, User item, int code = 0)
        {
            try
            {
                var c = 0;
                if (checkEmail(item.email))
                {
                    return RedirectToAction("Register");
                }
                if (code != 0)
                {
                    if (item != null)
                    {

                        if (int.TryParse(HttpContext.Session.GetObject<string>("code"),out c))
                        {
                            if (c == code)
                            {
                                if (Img != null)
                                {
                                    string folder = "Client/img/avatar/";
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
                                    item.avatar = "user.jpg";
                                }
                                item.password = Function.MD5Hash(item.password);
                                item.created_date = DateTime.Now;
                                var b = UserRepository.Instance.Create(item);
                                if (b == false)
                                {
                                    return RedirectToAction("Create");
                                }
                            }
                            else
                            {
                                item.avatar = "user.jpg";
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
            catch (Exception) { }
            return RedirectToAction("Index", "Login");
        }
    }
}