using Sehaty_Plus.Application.Common.Interfaces.Repositories;

namespace Sehaty_Plus.Application.Common.Interfaces.Persistence
{
    public interface IUnitOfWork
    {
        ISpecializationRepository Specializations { get; }
        IDoctorRepository Doctors { get; }
        IClinicRepository Clinics { get; }
        IDoctorClinicRepository DoctorClinics {  get; }

        Task<int> SaveChangesAsync(CancellationToken ct = default);
        Task BeginTransactionAsync(CancellationToken ct = default);
        Task CommitTransactionAsync(CancellationToken ct = default);
        Task RollbackTransactionAsync(CancellationToken ct = default);
    }
}
