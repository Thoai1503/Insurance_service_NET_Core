namespace Insurance_agency.Models.ModelView
{
    public class ContactPageView
    {
        public ContactEmail Contact { get; set; } = new ContactEmail();
        public List<InsuranceView> Insurance { get; set; } = new List<InsuranceView>();
    }
}
