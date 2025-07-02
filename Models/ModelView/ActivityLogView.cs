using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entities;

public partial class ActivityLogView
{
    public int id { get; set; } = 0;

    public int admin_id { get; set; } = 0;

    public string action { get; set; } = "";

    public string target_table { get; set; } = "";

    public int target_id { get; set; } = 0;

    public DateTime timestamp { get; set; } = DateTime.Now;

    public string detail { get; set; } = "";
}
