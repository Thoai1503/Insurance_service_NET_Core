using Insurance_agency.Models.ModelView;
using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.ModelView;

public partial class Policy
{
    public int id { get; set; } = 0;

    public int insurance_id { get; set; } = 0;

    public string name { get; set; } = "";

    public int age_min { get; set; } = 0;

    public int age_max { get; set; } = 0;

    
    public string description { get; set; } = "";

    public int active { get; set; } = 0;


    public InsuranceTypeView insurance_type { get; set; } 

    public  ICollection<ContractView> TblContracts { get; set; } = new HashSet<ContractView>();

    public  ICollection<PolicyBenifit> PolicyBenifits { get; set; } = new HashSet<PolicyBenifit>();
}
