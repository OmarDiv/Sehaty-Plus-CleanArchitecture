using Sehaty_Plus.Application.Common.Interfaces.Persistence;
using Sehaty_Plus.Application.Common.Interfaces.Services;

namespace Sehaty_Plus.Application.Feature.Specializations.Queries.GetAllSpecializationsDetailed
{
    public record GetAllSpecializationsDetailed() : IRequest<Result<IEnumerable<SpecializationDetailedResponse>>>;
    public class GetAllSpecializationsDetailedHandler(IUnitOfWork _unitOfWork, ICacheService _cacheService) : IRequestHandler<GetAllSpecializationsDetailed, Result<IEnumerable<SpecializationDetailedResponse>>>
    {
        private const string _cacheKey = "Specializations:AllDetailed";
        public async Task<Result<IEnumerable<SpecializationDetailedResponse>>> Handle(GetAllSpecializationsDetailed request, CancellationToken cancellationToken)
        {
            var data = await _cacheService.GetAsync(_cacheKey,
                factory: async () => await _unitOfWork.Specializations.GetAllDetailedAsync(cancellationToken)
                , cancellationToken);
            return Result.Success(data ?? []);
        }
    }
}