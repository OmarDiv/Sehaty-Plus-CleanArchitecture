using Sehaty_Plus.Application.Common.Interfaces.Persistence;
using Sehaty_Plus.Application.Common.Interfaces.Services;

namespace Sehaty_Plus.Application.Feature.Specializations.Queries.GetAllSpecialization;

public record GetAllSpecializations() : IRequest<Result<IEnumerable<SpecializationResponse>>>;

public class GetAllSpecializationsHandler(IUnitOfWork _unitOfWork, ICacheService _cacheService) : IRequestHandler<GetAllSpecializations, Result<IEnumerable<SpecializationResponse>>>
{
    private const string _cacheKey = "Specializations:All";
    public async Task<Result<IEnumerable<SpecializationResponse>>> Handle(GetAllSpecializations request, CancellationToken cancellationToken)
    {
        var data = await _cacheService.GetAsync(
                         _cacheKey,
                         factory: async () => await _unitOfWork.Specializations.GetAllActiveAsync(cancellationToken),
                         cancellationToken
        );
        return Result.Success(data ?? []);
    }
}