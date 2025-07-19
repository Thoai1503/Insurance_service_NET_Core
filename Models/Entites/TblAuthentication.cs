using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entities;

public partial class TblAuthentication
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int? Active { get; set; }

    public virtual ICollection<TblUser> TblUsers { get; set; } = new List<TblUser>();
}
