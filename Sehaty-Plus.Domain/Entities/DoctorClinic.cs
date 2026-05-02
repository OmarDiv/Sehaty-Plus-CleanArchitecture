using Hajj.Domain.Shared.Interfaces;

namespace Sehaty_Plus.Domain.Entities;

public class DoctorClinic : IEntity
{
    public long Id { get; set; }
    public long? DoctorId { get; set; }
    public long? ClinicId { get; set; }
    public bool IsPrimary { get; set; }
    public decimal? ConsultationFee { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Doctor Doctor { get; set; } = default!;
    public Clinic Clinic { get; set; } = default!;
}