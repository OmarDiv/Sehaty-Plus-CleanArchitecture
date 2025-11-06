namespace Sehaty_Plus.Application.Common.Interfaces
{
    public interface ISpecializationRepository : IGenericRepository<Specialization>
    {
        // EF
        Task<bool> ExistsByNameAsync(string name, int? excludeId = null, CancellationToken ct = default);

        // Dapper
        Task<IEnumerable<SpecializationResponse>> GetAllActiveAsync(CancellationToken ct = default);
        Task<IEnumerable<SpecializationDetailedResponse>> GetAllDetailedAsync(CancellationToken ct = default);
        Task<SpecializationResponse?> GetSpecializationByIdAsync(int id, CancellationToken ct = default);
        Task<SpecializationDetailedResponse?> GetSpecializationByIdDetailedAsync(int id, CancellationToken ct = default);

    }
}
