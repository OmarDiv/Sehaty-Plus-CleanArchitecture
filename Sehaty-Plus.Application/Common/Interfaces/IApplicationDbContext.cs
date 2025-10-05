namespace Sehaty_Plus.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Branch> Branches { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
