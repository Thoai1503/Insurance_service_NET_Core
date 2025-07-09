namespace Insurance_agency.Models.ModelView
{
    public class InsuranceView
    {
        public int id { get; set; } = 0;
        public string name { get; set; }=string.Empty;
        public string description { get; set; }=string.Empty;
        public int year_max { get; set; } = 0;
        public int value { get; set; } = 0;
        public int insurance_type_id { get; set; } = 0;
        public string ex_image {  get; set; }= string.Empty;
    }
}
