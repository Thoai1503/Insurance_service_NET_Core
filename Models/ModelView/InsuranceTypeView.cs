using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entities;

public partial class InsuranceTypeView
{
    public int id { get; set; }

    public string name { get; set; } = null!;

    public string description { get; set; }

    public int parent_id { get; set; }

    public HashSet<InsuranceTypeView> children { get; set; } = new HashSet<InsuranceTypeView>();
    public int active { get; set; }


}
