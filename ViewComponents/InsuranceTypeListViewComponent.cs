using Insurance_agency.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Insurance_agency.ViewComponents
{
    public class InsuranceTypeListViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var inList = await InsuranceTypeRepository.Instance.GetAll();
            return View(inList);
        }
    }
}
    