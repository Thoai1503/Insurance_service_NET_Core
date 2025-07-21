using System;
using System.Collections.Generic;

namespace Insurance_agency.Models.ModelView;

public class ContractView
{
    public int id { get; set; }

    public int? user_id { get; set; }

    public int? insurance_id { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public long? total_paid { get; set; }

    public long? year_paid { get; set; }

    public DateTime next_payment_due = DateTime.Now;

   public int employee_id { get; set; }
    public User employee { get; set; }
    public int? number_year_paid { get; set; }
    public long? value_contract { get; set; }

    public InsuranceView insurance { get; set; }
    public User user { get; set; }

    public HashSet<PaymentHistory> paymentHistories { get; set; } = new HashSet<PaymentHistory>();

    public int? status { get; set; }


}
