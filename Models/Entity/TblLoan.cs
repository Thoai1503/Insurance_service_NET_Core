using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entity;

public partial class TblLoan
{
    public int Id { get; set; }

    public int? ContractId { get; set; }

    public long? LoanAmount { get; set; }

    public DateTime? RequestDate { get; set; }

    public int? Status { get; set; }

    public virtual TblContract? Contract { get; set; }
}
