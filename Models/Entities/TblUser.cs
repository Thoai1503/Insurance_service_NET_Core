using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entities;

public partial class TblUser
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public int? AuthId { get; set; }

    public int? Active { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public virtual TblAuthentication? Auth { get; set; }

    public virtual ICollection<TblContract> TblContracts { get; set; } = new List<TblContract>();
}
