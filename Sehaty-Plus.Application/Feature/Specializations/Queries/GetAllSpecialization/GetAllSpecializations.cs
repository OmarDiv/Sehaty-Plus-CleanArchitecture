using Sehaty_Plus.Application.Feature.Specializations.Errors;
using Sehaty_Plus.Application.Feature.Specializations.Responses;

namespace Sehaty_Plus.Application.Feature.Specializations.Queries.GetAllSpecialization;

public record GetAllSpecializations() : IRequest<Result<IEnumerable<SpecializationResponse>>>;

public class GetAllSpecializationsHandler(IQueryExecuter queryExecuter) : IRequestHandler<GetAllSpecializations, Result<IEnumerable<SpecializationResponse>>>
{
    public async Task<Result<IEnumerable<SpecializationResponse>>> Handle(GetAllSpecializations request, CancellationToken cancellationToken)
    {
        var Response = await queryExecuter.Query<SpecializationResponse>("Select * from Specializations");
        if (Response is null)
            return Result.Failure<IEnumerable<SpecializationResponse>>(SpecializationErrors.SpecializationNotFound);
        return Result.Success(Response);
    }
}