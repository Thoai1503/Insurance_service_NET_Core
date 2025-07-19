namespace Insurance_agency.Models.ModelView
{
    public class InsuranceView
    {
        public InsuranceView() {
            name = string.Empty;
            description = string.Empty;
            ex_image = string.Empty;
            year_max = 0;
            value = 0;
            insurance_type_id = 0;
                



        }

        public int id { get; set; } = 0;
        public string name { get; set; }=string.Empty;
        public string description { get; set; }=string.Empty;
        public int year_max { get; set; } = 0;
        public int value { get; set; } = 0;
        public int insurance_type_id { get; set; } = 0;

        public int status { get; set; } = 1;
        public string ex_image {  get; set; }= string.Empty;
    }
}
