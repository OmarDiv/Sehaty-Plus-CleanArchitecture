using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Sehaty_Plus.Application.Common.Interfaces.Persistence;
using Sehaty_Plus.Application.Common.Interfaces.Repositories;
using Sehaty_Plus.Application.Common.Interfaces.Services;
using Sehaty_Plus.Infrastructure.Persistence;

namespace Sehaty_Plus.Infrastructure.Repositories
{
    public class UnitOfWork(ApplicationDbContext db, IQueryExecuter query) : IUnitOfWork
    {
        private readonly ApplicationDbContext _db = db;
        private readonly IQueryExecuter _query = query;

        private ISpecializationRepository? _specializations;
        private IDoctorRepository? _doctors;
        private IClinicRepository? _clinics;
        private IDoctorClinicRepository? _doctorClinics;

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct = default)
        {
            if (_db.Database.CurrentTransaction != null)
                return _db.Database.CurrentTransaction;

            return await _db.Database.BeginTransactionAsync(ct);
        }

        #region Repositories

        public ISpecializationRepository Specializations
        {
            get
            {
                return _specializations ??= new SpecializationRepository(_query, _db);
            }
        }
        public IDoctorRepository Doctors
        {
            get
            {
                return _doctors ??= new DoctorRepository(_query, _db);
            }
        }
        public IClinicRepository Clinics
        {
            get
            {
                return _clinics ??= new ClincRepository(_query, _db);
            }
        }
        public IDoctorClinicRepository DoctorClinics
        {
            get
            {
                return _doctorClinics ??= new DoctorClinicRepository(_query, _db);
            }
        }

        #endregion

        public Task<int> SaveChangesAsync(CancellationToken ct = default)
            => _db.SaveChangesAsync(ct);
    }
}