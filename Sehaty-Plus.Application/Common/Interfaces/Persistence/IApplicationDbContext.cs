using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Sehaty_Plus.Application.Common.Interfaces.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Specialization> Specializations { get; set; }
        DbSet<ApplicationUser> Users { get; set; }
        DbSet<Doctor> Doctors { get; set; }
        DbSet<Patient> Patients { get; set; }
        DbSet<Clinic> Clinics { get; set; }
        DbSet<DoctorClinic> DoctorClinics { get; set; }
        DbSet<Otp> Otps { get; set; }
        DbSet<ApplicationRole> Roles { get; set; }
        DbSet<IdentityRoleClaim<long>> RoleClaims { get; set; }
        ChangeTracker ChangeTracker { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
