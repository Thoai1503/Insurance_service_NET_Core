using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entity;

public partial class TblNotification
{
    public int Id { get; set; }

    public string? Detail { get; set; }

    public int? NotificationTypeId { get; set; }

    public int? Status { get; set; }

    public int ContractId { get; set; }

    public int? IsRead { get; set; }

    public virtual TblContract Contract { get; set; } = null!;
}
