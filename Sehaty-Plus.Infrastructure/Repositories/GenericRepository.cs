using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sehaty_Plus.Application.Common.Interfaces.Persistence;
using Sehaty_Plus.Application.Common.Interfaces.Repositories;
using Sehaty_Plus.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Sehaty_Plus.Infrastructure.Repositories
{
    public class GenericRepository<T>(IApplicationDbContext _db) : IGenericRepository<T> where T : class
    {
        protected readonly DbSet<T> _dbSet = _db.Set<T>();
        public virtual async Task<T?> GetByIdAsync(string id, CancellationToken ct = default)
            => await _dbSet.FindAsync(new { id }, ct);
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
        public async Task<List<T>> GetAll(CancellationToken cancellationToken = default)
            => await _dbSet.ToListAsync(cancellationToken);

        public async Task<bool> IsExist(Expression<Func<T, bool>> Criteria, CancellationToken cancellationToken = default)
            => await _dbSet.AnyAsync(Criteria, cancellationToken);

        public async Task<bool> Any(CancellationToken cancellationToken = default)
            => await _dbSet.AnyAsync(cancellationToken);

        public IQueryable<T> FromSqlRaw(string sql)
            => _dbSet.FromSqlRaw(sql);

        public IQueryable<T> AsQueryable()
            => _dbSet.AsQueryable();

        public async Task<List<T>> GetAll(Expression<Func<T, object>> Include, CancellationToken cancellationToken = default)
            => await _dbSet.Include(Include).ToListAsync(cancellationToken);

        public async Task<T> GetFirstAsync(CancellationToken cancellationToken = default)
            => await _dbSet.FirstOrDefaultAsync(cancellationToken) ?? Activator.CreateInstance<T>();

        public async Task<T?> GetByCriteria(Expression<Func<T, bool>> Criteria, CancellationToken cancellationToken = default)
            => await _dbSet.Where(Criteria).SingleOrDefaultAsync(cancellationToken);

        public async Task<List<T>> GetListByCriteria(Expression<Func<T, bool>> Criteria, CancellationToken cancellationToken = default)
            => await _dbSet.Where(Criteria).ToListAsync(cancellationToken);

        public async Task<List<T>> GetListByCriteria(Expression<Func<T, bool>> Criteria, Expression<Func<T, object>> Include, CancellationToken cancellationToken = default)
            => await _dbSet.Include(Include).Where(Criteria).ToListAsync(cancellationToken);

        public async Task<T?> GetByCriteria(Expression<Func<T, bool>> Criteria, Expression<Func<T, object>> Include, CancellationToken cancellationToken = default)
            => await _dbSet.Include(Include).FirstOrDefaultAsync(Criteria, cancellationToken);
        public async Task<T?> GetById(long id, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include, CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsQueryable();
            if (include != null)
                query = include(query);
            return await query.FirstOrDefaultAsync(e => EF.Property<long>(e, "Id") == id, cancellationToken);
        }

        public async Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
            => await _dbSet.FindAsync(new object[] { id }, cancellationToken);

    }
}

