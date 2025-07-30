﻿using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entities;

public partial class Insurance
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? YearMax { get; set; }

    public int? InsuranceTypeId { get; set; }

    public int? Value { get; set; }

    public int? Status { get; set; }

    public string? ExImage { get; set; }

    public virtual TblInsuranceType? InsuranceType { get; set; }

    public virtual ICollection<TblContract> TblContracts { get; set; } = new List<TblContract>();
}
