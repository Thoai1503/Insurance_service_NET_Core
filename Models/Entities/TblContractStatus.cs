using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entities;

public partial class TblContractStatus
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<TblContract> TblContracts { get; set; } = new List<TblContract>();
}
