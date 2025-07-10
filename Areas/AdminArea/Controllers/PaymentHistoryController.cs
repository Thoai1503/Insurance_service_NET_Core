using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Insurance_agency.Areas.AdminArea.Controllers
{
    public class PaymentHistoryController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.data = PaymentHistoryRepository.Instance.GetAll();
            return View();
        }
    }
}
