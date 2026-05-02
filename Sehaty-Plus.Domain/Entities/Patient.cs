using Sehaty_Plus.Domain.Enums;

namespace Sehaty_Plus.Domain.Entities
{
    public class Patient : ApplicationUser
    {
        public DateOnly? DateOfBirth { get; set; }
        public string Governorate { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public BloodType? BloodType { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? MedicalHistory { get; set; }
        public string? Allergies { get; set; }
    }
}


