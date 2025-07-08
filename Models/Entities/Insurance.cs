using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entities;

public partial class Insurance
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? TargetId { get; set; }

    public int? InsuranceTypeId { get; set; }

    public int? YearPaid { get; set; }

    public string? ExImage { get; set; }

    public virtual TblInsuranceType? InsuranceType { get; set; }

    public virtual ICollection<TblContract> TblContracts { get; set; } = new List<TblContract>();

    public virtual ICollection<TblPolicy> TblPolicies { get; set; } = new List<TblPolicy>();

    public virtual ICollection<TblSubsidiary> TblSubsidiaries { get; set; } = new List<TblSubsidiary>();
}
