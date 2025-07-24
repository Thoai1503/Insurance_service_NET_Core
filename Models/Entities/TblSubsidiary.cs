using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entity;

public partial class TblSubsidiary
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? InsuranceId { get; set; }

    public string? PromotionPercentage { get; set; }
}
