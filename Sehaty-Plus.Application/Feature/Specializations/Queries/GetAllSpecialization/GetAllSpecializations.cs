using Sehaty_Plus.Application.Common.Interfaces.Persistence;
using Sehaty_Plus.Application.Common.Interfaces.Services;

namespace Sehaty_Plus.Application.Feature.Specializations.Queries.GetAllSpecialization;

public record GetAllSpecializations() : IRequest<Result<IEnumerable<SpecializationResponse>>>;

public class GetAllSpecializationsHandler(IUnitOfWork _unitOfWork, ICacheService _cacheService) : IRequestHandler<GetAllSpecializations, Result<IEnumerable<SpecializationResponse>>>
{
    private const string CacheKey = "Specializations:All";
    public async Task<Result<IEnumerable<SpecializationResponse>>> Handle(GetAllSpecializations request, CancellationToken cancellationToken)
    {
        var result = await _cacheService.GetAsync(CacheKey,
            factory: async () =>
            {
                var Response = await _unitOfWork.Specializations.GetAllActiveAsync(cancellationToken);
                if (Response is null)
                    return Result.Failure<IEnumerable<SpecializationResponse>>(SpecializationErrors.SpecializationNotFound);
                return Result.Success(Response);
            },
            cancellationToken);

        return result!;
    }
}