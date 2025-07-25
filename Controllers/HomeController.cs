using Insurance_agency.Models;
using Insurance_agency.Models.Entities;
using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace Insurance_agency.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private InsuranceContext _context;
        private readonly IConfiguration _configuration;

        // Constructor injection for ILogger4
        // and InsuranceContext
        // to enable logging and database access
        public HomeController(InsuranceContext context, ILogger<HomeController> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }


        public async Task<IActionResult> Index()
        {
            // Assuming you have an extension method to set objects in session
            ViewBag.insurance = InsuranceRepository.Instance.GetAll();
            ViewBag.policy = PolicyRepository.Instance.GetAll().Count;
            ViewBag.employee = UserRepository.Instance.GetAllEmployeeUser().Count;
            var contract = await ContractRepository.Instance.GetAll();
            ViewBag.contract = contract.Count;
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
        [HttpGet]
        public IActionResult Contact()
        {
            var model = new ContactPageView
            {
                Insurance = _context.Insurances
                    .Select(x => new InsuranceView
                    {
                        id = x.Id,
                        name = x.Name
                    }).ToList(),
                Contact = new ContactEmail()
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(ContactPageView pageModel)
        {
            var model = pageModel.Contact;
            ViewBag.Message = "Your contact page.";
            pageModel.Insurance = _context.Insurances.Select(x => new InsuranceView
            {
                id = x.Id,
                name = x.Name,
            }).ToList();
            if (!ModelState.IsValid) return View(pageModel);
            var selectName = _context.Insurances.Where(i => model.SelectInsuranceId.Contains(i.Id)).Select(i => i.Name).ToList();
            string body = $@"
                <h2>Contact from customer</h2>
                <p><strong>Name:</strong> {model.Name}</p>
                <p><strong>Email:</strong> {model.Email}</p>
                <p><strong>Phone:</strong> {model.Phone}</p>
                <p><strong>Project:</strong> {model.Project}</p>
                <p><strong>Topic :</strong> {model.Subject}</p>
                <p><strong>Choosen Insurance:</strong> {string.Join(", ", selectName)}</p>
                <p><strong>Content:</strong><br/>{model.Message}</p>";
            try
            {
                var smtp = _configuration.GetSection("SMTP");
                var client = new SmtpClient
                {
                    Host = smtp["Host"],
                    Port = int.Parse(smtp["Port"]),
                    EnableSsl = bool.Parse(smtp["EnableSSL"]),
                    Credentials = new NetworkCredential(smtp["UserName"], smtp["Password"])
                };
                var mail = new MailMessage
                {
                    From = new MailAddress(smtp["UserName"], "Website Contact"),
                    Subject = model.Subject ?? "Lieen hệ từ website",
                    Body = body,
                    IsBodyHtml = true
                };
                mail.To.Add(smtp["UserName"]);
                await client.SendMailAsync(mail);
                TempData["Success"] = "Sent Success";
                return RedirectToAction("Contact");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error Send Email: " + ex.Message);
                return View(pageModel);
            }
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
        public IActionResult InsuranceOverview(string? search, int page = 1)
        {


            HttpContext.Session.SetInt32("allbanner", 0);
            var insuranceList = InsuranceRepository.Instance.GetAll().Where(e => e.status == 1).ToHashSet();
            int pageSize = 9;
            int previousPage = page - 2;
            int nextPage = page + 2;
            int firstPage = 1;
            var totalItems = insuranceList.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            insuranceList = insuranceList
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToHashSet();
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {

                if (!string.IsNullOrEmpty(search))
                {
                    insuranceList = InsuranceRepository.Instance.FindByKeywork(search);
                }
                return Json(new { message = "AJAX request", insuranceList });
            }

            if (previousPage > 1)
            {
                ViewBag.PreviousPage = previousPage;
            }
            else
            {
                ViewBag.PreviousPage = 1;
            }
            ViewBag.TotalPages = 5;

            ViewBag.ttPages = totalPages;
            ViewBag.CurrentPage = page;

            ViewBag.Search = search;

            ViewBag.BannerCss = "motobike";
            return View(insuranceList);
        }
        public IActionResult InsuranceDetail(int id)
        {
            var insurance = InsuranceRepository.Instance.FindById(id);


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
