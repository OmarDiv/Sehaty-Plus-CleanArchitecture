using Microsoft.EntityFrameworkCore.Storage;

namespace Sehaty_Plus.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<DoctorClinic> DoctorClinics { get; set; }
        public DbSet<Otp> Otps { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
