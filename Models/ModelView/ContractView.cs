using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.ModelView;

public class ContractView
{
    public int id { get; set; }

    public int? user_id { get; set; }

    public int? policy_id { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public long? TotalPaid { get; set; }

    public int? Status { get; set; }


}
