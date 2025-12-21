using Sehaty_Plus.Application.Common.Interfaces.Persistence;
using Sehaty_Plus.Application.Common.Interfaces.Repositories;
using Sehaty_Plus.Application.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sehaty_Plus.Infrastructure.Repositories
{
    public class DoctorRepository(IQueryExecuter _queryExecuter,IApplicationDbContext _db) : GenericRepository<Doctor>(_db), IDoctorRepository
    {
    }
}
