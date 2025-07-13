using Insurance_agency.Models.ModelView;
using Insurance_agency.Services.VnPay;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Controllers
{
    public class PaymentController : Controller
    {

        private readonly IVnPayService _vnPayService;
        public PaymentController(IVnPayService vnPayService)
        {

            _vnPayService = vnPayService;
        }

        public IActionResult CreatePaymentUrlVnpay(PaymentInformationModel model,ContractView ctr)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
        }

    }
}
