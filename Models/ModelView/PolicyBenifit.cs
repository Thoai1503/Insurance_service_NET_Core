using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entities;

public partial class PolicyBenifit
{
    public int id { get; set; }

    public int policy_id { get; set; }

    public string detail { get; set; }

    public long benifit_amount { get; set; }

    public int active { get; set; }

    public Policy policy { get; set; }
}
