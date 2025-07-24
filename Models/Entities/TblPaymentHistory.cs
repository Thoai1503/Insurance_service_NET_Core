using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entities;

public partial class TblPaymentHistory
{
    public int Id { get; set; }

    public int? ContractId { get; set; }

    public DateTime? PaymentDay { get; set; }

    public long? Amount { get; set; }

    public int? Status { get; set; }

    public virtual TblContract? Contract { get; set; }
}
