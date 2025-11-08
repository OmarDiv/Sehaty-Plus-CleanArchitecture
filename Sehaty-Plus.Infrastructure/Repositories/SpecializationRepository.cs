using Microsoft.EntityFrameworkCore;
using Sehaty_Plus.Application.Common.Interfaces;
using Sehaty_Plus.Application.Feature.Specializations.Responses;
using Sehaty_Plus.Application.Services.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sehaty_Plus.Infrastructure.Repositories
{
    public class SpecializationRepository(IQueryExecuter _queryExecuter, IApplicationDbContext _db) : GenericRepository<Specialization>(_db), ISpecializationRepository
    {
        public async Task<bool> ExistsByNameAsync(string name, int? excludeId = null, CancellationToken ct = default)
        {
            return await _dbSet.AnyAsync(
                s => s.Name == name && (excludeId == null || s.Id != excludeId.Value),
                ct);
        }

        public async Task<IEnumerable<SpecializationResponse>> GetAllActiveAsync(CancellationToken ct = default)
        {
            var sql = "SELECT Id, Name, Description FROM Specializations WHERE IsActive = 1";
            return await _queryExecuter.Query<SpecializationResponse>(sql);
        }
        public async Task<SpecializationResponse?> GetSpecializationByIdAsync(int id, CancellationToken ct = default)
        {
            var sql = "SELECT Id, Name, Description FROM Specializations WHERE Id = @id";
            return await _queryExecuter.QueryFirstOrDefault<SpecializationResponse>(sql, new { Id = id });
        }
        public async Task<IEnumerable<SpecializationDetailedResponse>> GetAllDetailedAsync(CancellationToken ct = default)
        {
            var sql = "SELECT * FROM Specializations";
            return await _queryExecuter.Query<SpecializationDetailedResponse>(sql);
        }
        public async Task<SpecializationDetailedResponse?> GetSpecializationByIdDetailedAsync(int id, CancellationToken ct = default)
        {
            var sql = "Select * from Specializations where Id = @Id";
            return await _queryExecuter.QueryFirstOrDefault<SpecializationDetailedResponse>(sql, new { Id = id });
        }
    }
}
