using System.Diagnostics;
using Insurance_agency.Models;
using Insurance_agency.Models.Entity;
using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private InsuranceContext _context;

        // Constructor injection for ILogger4
        // and InsuranceContext
        // to enable logging and database access
        public HomeController(InsuranceContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }


        public IActionResult Index()
        {
            
            var insurList = InsuranceTypeRepository.Instance.GetAll();

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
            var id = Request.Query["id"].ToString();
            ViewBag.Message = "Your insurance page.";
            return View();
        }
        public IActionResult InsuranceOverview()
        {

            HttpContext.Session.SetInt32("allbanner", 0);
            var insuranceList = InsuranceRepository.Instance.GetAll().Take(3).ToHashSet();
            ViewBag.BannerCss = "motobike";
            return View(insuranceList);
        }
        public IActionResult InsuranceDetail()
        {

            HttpContext.Session.SetInt32("allbanner", 0);

           ViewBag.BannerCss = "motobike";
            return View();
        }
    }
}
    