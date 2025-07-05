namespace Insurance_agency.Models.ModelView
{
    public class InsuranceView
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; }=string.Empty;
        public string Description { get; set; }=string.Empty;
        public int TargetId { get; set; } = 0;
        public int InsuranceTypeId { get; set; } = 0;
        public string ExImage {  get; set; }= string.Empty;
    }
}
