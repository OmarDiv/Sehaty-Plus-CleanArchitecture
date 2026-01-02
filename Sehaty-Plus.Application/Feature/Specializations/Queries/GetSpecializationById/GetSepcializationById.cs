using Sehaty_Plus.Application.Common.Interfaces.Persistence;
using Sehaty_Plus.Application.Common.Interfaces.Services;

namespace Sehaty_Plus.Application.Feature.Specializations.Queries.GetSpecializationById;

public record GetSpecializationByIdDetailed(int Id) : IRequest<Result<SpecializationResponse>>;
public class GetSpecializtionByIdHandler(IUnitOfWork _unitOfWork, ICacheService _cacheService) : IRequestHandler<GetSpecializationByIdDetailed, Result<SpecializationResponse>>
{
    private static string CacheKey(int id)
            => $"Specializations:Id:{id}";
    public async Task<Result<SpecializationResponse>> Handle(GetSpecializationByIdDetailed request, CancellationToken cancellationToken)
    {
        var data = await _cacheService.GetAsync(CacheKey(request.Id),
            factory: async () => (await _unitOfWork.Specializations.GetSpecializationByIdAsync(request.Id, cancellationToken))!,
             cancellationToken
        );

        if (data is null)
            return Result.Failure<SpecializationResponse>(SpecializationErrors.SpecializationNotFound);
        return Result.Success(data);

    }
}
