using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Sehaty_Plus.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Specialization> Specializations { get; set; }
        DbSet<Doctor> Doctors { get; set; }
        DbSet<Patient> Patients { get; set; }
        DbSet<Clinic> Clinics { get; set; }
        DbSet<DoctorClinic> DoctorClinics { get; set; }
        DbSet<Otp> Otps { get; set; }
        DbSet<ApplicationRole> Roles { get; set; }
        DbSet<IdentityRoleClaim<string>> RoleClaims { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
