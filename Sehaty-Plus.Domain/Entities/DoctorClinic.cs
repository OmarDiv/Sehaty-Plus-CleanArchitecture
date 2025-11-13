namespace Sehaty_Plus.Domain.Entities;

public class DoctorClinic
{
    public int Id { get; set; }
    public string DoctorId { get; set; } = string.Empty;
    public int ClinicId { get; set; }
    public bool IsPrimary { get; set; }
    public decimal? ConsultationFee { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Doctor Doctor { get; set; } = default!;
    public Clinic Clinic { get; set; } = default!;
}