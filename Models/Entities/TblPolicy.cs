using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entity;

public partial class TblPolicy
{
    public int Id { get; set; }

    public int? InsuranceId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? AgeMin { get; set; }

    public int? AgeMax { get; set; }

    public int? Active { get; set; }

    public virtual Insurance? Insurance { get; set; }
}
