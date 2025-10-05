namespace Sehaty_Plus.Domain.Entities
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string NationalId { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? BloodType { get; set; }
        public string? EmergencyContact { get; set; }
        public string? MedicalHistory { get; set; }
        public string? Allergies { get; set; }

        public ApplicationUser User { get; set; } = default!;
    }
}


