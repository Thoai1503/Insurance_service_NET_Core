using Insurance_agency.Models.ModelView;
using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.Entities;

public partial class Policy
{
    public int id { get; set; } = 0;

    public int insurance_type_id { get; set; } = 0;

    public string name { get; set; } = "";

    public int term_min { get; set; } = 0;

    public int term_max { get; set; } = 0;

    public long sum_assured { get; set; } = 0;

    public int active { get; set; } = 0;

    public InsuranceTypeView insurance_type { get; set; } 

    public  ICollection<ContractView> TblContracts { get; set; } = new HashSet<ContractView>();

    public  ICollection<PolicyBenifit> PolicyBenifits { get; set; } = new HashSet<PolicyBenifit>();
}
