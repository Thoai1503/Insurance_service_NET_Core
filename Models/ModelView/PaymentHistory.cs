using Insurance_agency.Models.ModelView;
using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.ModelView;

public partial class PaymentHistory
{
    public int id { get; set; } = 0;

    public int contract_id { get; set; } = 0;

    public DateTime payment_day { get; set; } = DateTime.Now;

    public long amount { get; set; } = 0;

    public int Status { get; set; } = 0;

    public  ContractView Contract { get; set; } 

}
