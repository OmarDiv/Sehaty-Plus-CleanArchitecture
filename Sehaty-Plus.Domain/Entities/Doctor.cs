namespace Sehaty_Plus.Domain.Entities
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
       // public int SpecializationId { get; set; }
        public int YearsOfExperience { get; set; }
        public string? Education { get; set; }
        public string? Biography { get; set; }
        public decimal ConsultationFee { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; } = default!;
        public bool IsVerified { get; set; } = false;

        public ApplicationUser User { get; set; } = default!;
        //public Specialization Specialization { get; set; } = default!;

    }

}
