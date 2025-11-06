using Microsoft.EntityFrameworkCore;
using Sehaty_Plus.Application.Common.Interfaces;

namespace Sehaty_Plus.Infrastructure.Repositories
{
    public class GenericRepository<T>(IApplicationDbContext _db) : IGenericRepository<T> where T : class
    {
        protected readonly DbSet<T> _dbSet = _db.Set<T>();
        public virtual async Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
            => await _dbSet.FindAsync(new { id }, ct);
        public virtual async Task<T> AddAsync(T entity, CancellationToken ct = default)
        {
            await _dbSet.AddAsync(entity, ct);
            return entity;
        }
        public virtual Task UpdateAsync(T entity, CancellationToken ct = default)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }
        public virtual Task DeleteAsync(T entity, CancellationToken ct = default)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }
    }
}
