using Microsoft.EntityFrameworkCore.Storage;
using Sehaty_Plus.Application.Common.Interfaces;
using Sehaty_Plus.Application.Services.Queries;

namespace Sehaty_Plus.Infrastructure.Repositories
{
    public class UnitOfWork(IApplicationDbContext db, IQueryExecuter query) : IUnitOfWork
    {
        private readonly IApplicationDbContext _db = db;
        private readonly IQueryExecuter _query = query;
        private IDbContextTransaction? _transaction;

        private ISpecializationRepository? _specializations;

        public ISpecializationRepository Specializations
        {
            get
            {
                return _specializations ??= new SpecializationRepository(_query, _db);
            }
        }

        public Task<int> SaveChangesAsync(CancellationToken ct = default)
            => _db.SaveChangesAsync(ct);

        public async Task BeginTransactionAsync(CancellationToken ct = default)
        {
            if (_transaction != null) return;
            _transaction = await _db.BeginTransactionAsync(ct);  // ✅ ينشأ من DbContext
        }

        public async Task CommitTransactionAsync(CancellationToken ct = default)
        {
            try
            {
                await _db.SaveChangesAsync(ct);
                if (_transaction != null)
                    await _transaction.CommitAsync(ct);
            }
            catch
            {
                await RollbackTransactionAsync(ct);
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken ct = default)
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync(ct);
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}