namespace Insurance_agency.Models.ModelView
{
    public class AssignHistoryView
    {
        public int id { get; set; }

        public int employee_id { get; set; } = 0;

        public int contract_id { get; set; } = 0;

        public DateTime start_date { get; set; } 

        public DateTime? end_date { get; set; }
    }
}
