using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Insurance_agency.ViewComponents
{
    public class InsuranceTypeListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // Call the controller's List method to get the insurance types
     //      var inList = InsuranceTypeRepository.Instance.GetAll();
            return View();
        }
    }
}
    