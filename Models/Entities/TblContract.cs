using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entities;

public partial class TblContract
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? PolicyId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public long? TotalPaid { get; set; }

    public int? Status { get; set; }

    public virtual TblPolicy? Policy { get; set; }

    public virtual TblContractStatus? StatusNavigation { get; set; }

    public virtual ICollection<TblLoan> TblLoans { get; set; } = new List<TblLoan>();

    public virtual ICollection<TblPaymentHistory> TblPaymentHistories { get; set; } = new List<TblPaymentHistory>();

    public virtual TblUser? User { get; set; }
}
