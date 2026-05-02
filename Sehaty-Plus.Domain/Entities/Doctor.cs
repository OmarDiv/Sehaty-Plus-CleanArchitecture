using Sehaty_Plus.Domain.Enums;

namespace Sehaty_Plus.Domain.Entities
{
    public class Doctor : ApplicationUser
    {
        public string LicenseNumber { get; set; } = string.Empty;
        public long? SpecializationId { get; set; }
        public int? YearsOfExperience { get; set; }
        public DoctorTitle? Title { get; set; }
        public string? Education { get; set; }
        public string? Biography { get; set; }
        public bool IsVerified { get; set; } = false;
        public Specialization? Specialization { get; set; }
        public ICollection<DoctorClinic> DoctorClinics { get; set; } = [];
    }
}