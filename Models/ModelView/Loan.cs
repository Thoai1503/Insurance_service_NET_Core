using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.ModelView;

public partial class Loan
{
    public int Id { get; set; }

    public int? ContractId { get; set; }

    public long? LoanAmount { get; set; }

    public DateTime? RequestDate { get; set; }

    public int? Status { get; set; }

    public virtual ContractView? Contract { get; set; }

}
