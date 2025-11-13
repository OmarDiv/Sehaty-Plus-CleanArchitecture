namespace Sehaty_Plus.Domain.Entities
{
    public class Patient
    {
        public string Id { get; set; } = Guid.CreateVersion7().ToString();
        public string UserId { get; set; } = string.Empty;
        public string NationalId { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Governorate { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string? BloodType { get; set; }
        public string? EmergencyContact { get; set; }
        public string? MedicalHistory { get; set; }
        public string? Allergies { get; set; }

        public ApplicationUser User { get; set; } = default!;
    }
}


