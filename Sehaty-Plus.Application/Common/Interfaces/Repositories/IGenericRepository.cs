namespace Sehaty_Plus.Application.Common.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(string id, CancellationToken ct = default);
        Task<T?> GetByIdAsync(int id, CancellationToken ct = default);

        Task<T> AddAsync(T entity, CancellationToken ct = default);
        Task UpdateAsync(T entity, CancellationToken ct = default);
        Task DeleteAsync(T entity, CancellationToken ct = default);
    }
}
