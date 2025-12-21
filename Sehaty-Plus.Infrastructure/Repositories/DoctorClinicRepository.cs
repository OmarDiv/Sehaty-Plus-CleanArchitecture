using Sehaty_Plus.Application.Common.Interfaces.Persistence;
using Sehaty_Plus.Application.Common.Interfaces.Repositories;
using Sehaty_Plus.Application.Common.Interfaces.Services;

namespace Sehaty_Plus.Infrastructure.Repositories
{
    public class DoctorClinicRepository(IQueryExecuter query, IApplicationDbContext _db) : GenericRepository<DoctorClinic>(_db), IDoctorClinicRepository
    {
    }
}
