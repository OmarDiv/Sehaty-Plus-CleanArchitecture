namespace Sehaty_Plus.Application.Common.Interfaces.Repositories
{
    public interface IClinicRepository : IGenericRepository<Clinic>
    {
        //EF
        Task<bool> ExistsByNameAsync(string name, int? excludeId = null, CancellationToken ct = default);
        
    }
}
