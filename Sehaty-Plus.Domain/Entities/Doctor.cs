namespace Sehaty_Plus.Domain.Entities;

public class Doctor
{
    public string Id { get; set; } = Guid.CreateVersion7().ToString();
    public string UserId { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public int SpecializationId { get; set; }
    public int YearsOfExperience { get; set; }
    public string? Education { get; set; }
    public string? Biography { get; set; }
    public bool IsVerified { get; set; } = false;
    public ApplicationUser User { get; set; } = default!;
    public Specialization Specialization { get; set; } = default!;
    public ICollection<DoctorClinic> DoctorClinics { get; set; } = [];
}
