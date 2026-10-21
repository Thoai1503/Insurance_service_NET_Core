using System.ComponentModel.DataAnnotations;

namespace Insurance_agency.Models.ModelView;

public class InsuranceTypeCreateViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập tên loại bảo hiểm.")]
    [StringLength(50, ErrorMessage = "Tên loại bảo hiểm không được vượt quá 50 ký tự.")]
    [Display(Name = "Tên loại bảo hiểm")]
    public string? Name { get; set; }

    [StringLength(200, ErrorMessage = "Mô tả không được vượt quá 200 ký tự.")]
    [Display(Name = "Mô tả")]
    public string? Description { get; set; }

    [Display(Name = "Loại bảo hiểm cha")]
    public int ParentId { get; set; }

    [StringLength(100, ErrorMessage = "CSS class không được vượt quá 100 ký tự.")]
    [Display(Name = "CSS class (không bắt buộc)")]
    public string? ImageClassCss { get; set; }
}
