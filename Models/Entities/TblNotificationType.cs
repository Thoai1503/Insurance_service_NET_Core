using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entities;

public partial class TblNotificationType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<TblNotification> TblNotifications { get; set; } = new List<TblNotification>();
}
