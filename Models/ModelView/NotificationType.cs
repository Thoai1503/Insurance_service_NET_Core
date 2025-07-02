using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entities;

public partial class NotificationType
{
    public int id { get; set; }

    public string name { get; set; }

    public int status { get; set; }

    public virtual ICollection<Notification> TblNotifications { get; set; } = new List<Notification>();
}
