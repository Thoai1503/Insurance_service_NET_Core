using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entities;

public partial class TblContract
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? InsuranceId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal? ValueContract { get; set; }

    public decimal? TotalPaid { get; set; }

    public decimal? YearPaid { get; set; }

    public int? NumberYearPaid { get; set; }

    public int? Status { get; set; }

    public virtual Insurance? Insurance { get; set; }

    public virtual ICollection<TblLoan> TblLoans { get; set; } = new List<TblLoan>();

    public virtual ICollection<TblNotification> TblNotifications { get; set; } = new List<TblNotification>();

    public virtual ICollection<TblPaymentHistory> TblPaymentHistories { get; set; } = new List<TblPaymentHistory>();

    public virtual TblUser? User { get; set; }
}
