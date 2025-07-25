using System.ComponentModel.DataAnnotations;

namespace Insurance_agency.Models.ModelView
{
    public class ContactEmail
    {
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Project { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public List<int> SelectInsuranceId { get; set; } = new List<int>();
    }
}
