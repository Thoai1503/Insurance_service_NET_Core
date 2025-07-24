using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Insurance_agency.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
           return View();
        }
        public ActionResult Acc_check()
        {
           
            string username = Request.Form["username"];
            string password = Request.Form["password"];
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Index", new { username, password });
            }
            User member = MiddleWareRepository.Instance.CheckLogin(username, password);
            if (member != null)
            {
                HttpContext.Session.SetObject("user", member);
          
                switch (member.auth_id)
                {
                    case 1:
                        return RedirectToAction("Dashboard", "AdminArea", new { member });
                    case 3:
                        return RedirectToAction("Dashboard", "AdminArea", new { member });
                    case 4:
                        return RedirectToAction("Index", "Home", new { member });

                }
            }

            var message = "Invalid email or wrong password";
            return RedirectToAction("Index", "Login", new { message });
        }
    }
}
