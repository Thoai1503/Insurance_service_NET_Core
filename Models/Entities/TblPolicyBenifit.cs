using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entities;

public partial class TblPolicyBenifit
{
    public int Id { get; set; }

    public int? PolicyId { get; set; }

    public string? Detail { get; set; }

    public long? BenifitAmount { get; set; }

    public int? Active { get; set; }

    public virtual TblPolicy? Policy { get; set; }
}
