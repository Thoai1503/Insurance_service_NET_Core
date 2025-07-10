using Insurance_agency.Models.ModelView;
using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class PolicyManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create(Policy en)
        {

            var rs = PolicyRepository.Instance.Create(en);

            if (rs)
            {
                return Ok(rs);
            }
            else
            {
                // Handle the case where creation failed, e.g., return an error message
                return Ok(false);
            }

        }
        public IActionResult Delete(int id)
        {
            var rs = PolicyRepository.Instance.Delete(id);
            if (rs)
            {
                return Ok(rs);
            }
            else
            {
                // Handle the case where deletion failed, e.g., return an error message
                return Ok(false);
            }
        }
    }
}
