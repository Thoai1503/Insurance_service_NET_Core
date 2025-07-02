using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entities;

public partial class TblNotification
{
    public int Id { get; set; }

    public string? Detail { get; set; }

    public int? NotificationTypeId { get; set; }

    public int? Status { get; set; }

    public int UserId { get; set; }

    public int? IsRead { get; set; }

    public virtual TblNotificationType? NotificationType { get; set; }

    public virtual TblUser User { get; set; } = null!;
}
