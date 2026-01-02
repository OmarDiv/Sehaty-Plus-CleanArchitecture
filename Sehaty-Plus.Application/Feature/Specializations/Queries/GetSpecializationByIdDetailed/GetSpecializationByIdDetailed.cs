using Sehaty_Plus.Application.Common.Interfaces.Persistence;
using Sehaty_Plus.Application.Common.Interfaces.Services;

namespace Sehaty_Plus.Application.Feature.Specializations.Queries.GetSpecializationByIdDetailed
{
    public record GetSpecializationByIdDetailed(int Id) : IRequest<Result<SpecializationDetailedResponse>>;
    public class GetSpecializationByIdDetailedHandler(IUnitOfWork _unitOfWork, ICacheService _cacheService) : IRequestHandler<GetSpecializationByIdDetailed, Result<SpecializationDetailedResponse>>
    {
        private static string CacheKey(int id)
            => $"Specializations:Id:{id}";
        public async Task<Result<SpecializationDetailedResponse>> Handle(GetSpecializationByIdDetailed request, CancellationToken cancellationToken)
        {
            var data = await _cacheService.GetAsync<SpecializationDetailedResponse>(
                CacheKey(request.Id),
                factory: async () => (await _unitOfWork.Specializations.GetSpecializationByIdDetailedAsync(request.Id, cancellationToken))!,
                cancellationToken
                );
            if (data is null)
                return Result.Failure<SpecializationDetailedResponse>(SpecializationErrors.SpecializationNotFound);
            return Result.Success(data);
        }
    }
}
