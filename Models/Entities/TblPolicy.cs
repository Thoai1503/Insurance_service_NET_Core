using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entities;

public partial class TblPolicy
{
    public int Id { get; set; }

    public int? InsuranceTypeId { get; set; }

    public string? Name { get; set; }

    public int? TermMin { get; set; }

    public int? TermMax { get; set; }

    public long? SumAssured { get; set; }

    public int? Active { get; set; }

    public virtual TblInsuranceType? InsuranceType { get; set; }

    public virtual ICollection<TblContract> TblContracts { get; set; } = new List<TblContract>();

    public virtual ICollection<TblPolicyBenifit> TblPolicyBenifits { get; set; } = new List<TblPolicyBenifit>();
}
