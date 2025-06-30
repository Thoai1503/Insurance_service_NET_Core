using System.Diagnostics;
using Insurance_agency.Models;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public IActionResult Services()
        {
            HttpContext.Session.SetInt32("display", 0);
            ViewBag.Message = "Your services page.";
            return View();
        }
        public IActionResult Blog()
        {
            ViewBag.Message = "Your privacy policy page.";
            return View();
        }
        public IActionResult Feature()
        {
            ViewBag.Message = "Your privacy policy page.";
            return View();
        }
        public IActionResult Team()
        {
            ViewBag.Message = "Your privacy policy page.";
            return View();
        }
        public IActionResult Testimonial()
        {
            ViewBag.Message = "Your privacy policy page.";
            return View();
        }
        public IActionResult DemoForm()
        {
            ViewBag.Message = "Your privacy policy page.";
            return View();
        }
        public IActionResult TextEditor()
        {
            ViewBag.Message = "Your text editor page.";
            return View();
        }
        public IActionResult Insurance()
        {
            HttpContext.Session.SetInt32("allbanner", 0); // Assuming you want to use session state
            //   Session["display"] = 0;
            ViewBag.Message = "Your insurance page.";
            return View();
        }
    }
}
    