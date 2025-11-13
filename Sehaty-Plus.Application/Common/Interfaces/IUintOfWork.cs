namespace Sehaty_Plus.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        ISpecializationRepository Specializations { get; }
        Task<int> SaveChangesAsync(CancellationToken ct = default);
        Task BeginTransactionAsync(CancellationToken ct = default);
        Task CommitTransactionAsync(CancellationToken ct = default);
        Task RollbackTransactionAsync(CancellationToken ct = default);
    }
}
