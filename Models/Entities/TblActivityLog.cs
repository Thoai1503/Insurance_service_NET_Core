using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entities;

public partial class TblActivityLog
{
    public string Id { get; set; } = null!;

    public int? AdminId { get; set; }

    public string? Action { get; set; }

    public string? TargetTable { get; set; }

    public int? TargetId { get; set; }

    public DateTime? Timestamp { get; set; }

    public string? Detail { get; set; }
}
