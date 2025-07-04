using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entities;

public partial class TblPolicy
{
    public int Id { get; set; }

    public int? InsuranceId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? TermMin { get; set; }

    public int? TermMax { get; set; }

    public int? Active { get; set; }

    public virtual Insurance? Insurance { get; set; }
}
