using Sehaty_Plus.Application.Common.Interfaces.Persistence;
using Sehaty_Plus.Application.Common.Interfaces.Repositories;
using Sehaty_Plus.Application.Common.Interfaces.Services;

namespace Sehaty_Plus.Infrastructure.Repositories
{
    public class ClincRepository(IQueryExecuter query, IApplicationDbContext _context) : GenericRepository<Clinic>(_context), IClinicRepository
    {
        private readonly IQueryExecuter _query = query;

        public Task<bool> ExistsByNameAsync(string name, int? excludeId = null, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
        //public override Task<> AddAsync(Clinic entity, CancellationToken ct = default)
        //{
        //    // Custom logic before adding a Clinic entity can be placed here
        //    return base.AddAsync(entity, ct);
        //}
    }
}
