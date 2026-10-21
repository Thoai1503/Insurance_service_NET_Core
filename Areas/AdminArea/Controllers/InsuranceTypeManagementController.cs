using Insurance_agency.Models.Entities;
using Insurance_agency.Models.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Insurance_agency.Areas.AdminArea.Controllers;

[Area("AdminArea")]
public class InsuranceTypeManagementController : Controller
{
    private readonly InsuranceContext _context;

    public InsuranceTypeManagementController(InsuranceContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        await PopulateViewDataAsync();
        ViewBag.InsuranceTypes = await _context.TblInsuranceTypes
            .OrderBy(type => type.Name)
            .ToListAsync();
        return View(new InsuranceTypeCreateViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(InsuranceTypeCreateViewModel model)
    {
        var name = model.Name?.Trim();
        if (!string.IsNullOrEmpty(name) &&
            await _context.TblInsuranceTypes.AnyAsync(type => type.Name.ToLower() == name.ToLower()))
        {
            ModelState.AddModelError(nameof(model.Name), "Tên loại bảo hiểm này đã tồn tại.");
        }

        if (model.ParentId != 0 &&
            !await _context.TblInsuranceTypes.AnyAsync(type => type.Id == model.ParentId && type.Active == 1))
        {
            ModelState.AddModelError(nameof(model.ParentId), "Loại bảo hiểm cha không hợp lệ.");
        }

        if (!ModelState.IsValid)
        {
            await PopulateViewDataAsync();
            ViewBag.InsuranceTypes = await _context.TblInsuranceTypes
                .OrderBy(type => type.Name)
                .ToListAsync();
            return View("Index", model);
        }

        _context.TblInsuranceTypes.Add(new TblInsuranceType
        {
            Name = name!,
            Description = string.IsNullOrWhiteSpace(model.Description) ? null : model.Description.Trim(),
            ParentId = model.ParentId,
            Active = 1,
            ImageClassCss = string.IsNullOrWhiteSpace(model.ImageClassCss) ? null : model.ImageClassCss.Trim()
        });
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Đã tạo loại bảo hiểm.";
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateViewDataAsync()
    {
        ViewBag.ParentTypes = await _context.TblInsuranceTypes
            .Where(type => type.Active == 1)
            .OrderBy(type => type.Name)
            .ToListAsync();
    }
}
