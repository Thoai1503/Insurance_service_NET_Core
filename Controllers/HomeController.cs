using Insurance_agency.Models;
using Insurance_agency.Models.Entities;
using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
         // Assuming you have an extension method to set objects in session

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
        public IActionResult Insurance(int typeId)
        {
            HttpContext.Session.SetInt32("allbanner", 0); // Assuming you want to use session state
            //   Session["display"] = 0;
            var id = Request.Query["id"].ToString();
            ViewBag.data = InsuranceRepository.Instance.FindByInsuranceTypeId(typeId);
            ViewBag.name = InsuranceTypeRepository.Instance.FindById(typeId).name;
            ViewBag.Message = "Your insurance page.";

            return View();
        }
        public IActionResult InsuranceOverview()
        {

            HttpContext.Session.SetInt32("allbanner", 0);
            var insuranceList = InsuranceRepository.Instance.GetAll().ToHashSet();
            ViewBag.BannerCss = "motobike";
            return View(insuranceList);
        }
        public IActionResult InsuranceDetail(int id)
        {
            var insurance = InsuranceRepository.Instance.FindById(id);
            User user = HttpContext.Session.GetObject<User>("user");
            ViewBag.user = user;

            var relatedinsurance = InsuranceRepository.Instance.FindByInsuranceTypeId(insurance.insurance_type_id).Take(3).ToHashSet();
            var item = PolicyRepository.Instance.GetAllByInsuranceId(id);

            HttpContext.Session.SetInt32("allbanner", 0);

           ViewBag.BannerCss = "motobike";
            ViewBag.Insurance = insurance;
            ViewBag.RelatedInsurance = relatedinsurance;
            return View(item);
        }
    }
}
    