using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entities;

public partial class TblInsuranceType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int ParentId { get; set; }

    public int Active { get; set; }

    public string? ImageClassCss { get; set; }

    public virtual ICollection<Insurance> Insurances { get; set; } = new List<Insurance>();
}
