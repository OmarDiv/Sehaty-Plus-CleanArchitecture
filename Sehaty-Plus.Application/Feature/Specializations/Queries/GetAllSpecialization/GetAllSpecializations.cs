namespace Sehaty_Plus.Application.Feature.Specializations.Queries.GetAllSpecialization;

public record GetAllSpecializations() : IRequest<Result<IEnumerable<SpecializationResponse>>>;

public class GetAllSpecializationsHandler(IQueryExecuter _queryExecuter) : IRequestHandler<GetAllSpecializations, Result<IEnumerable<SpecializationResponse>>>
{
    public async Task<Result<IEnumerable<SpecializationResponse>>> Handle(GetAllSpecializations request, CancellationToken cancellationToken)
    {
        var Response = await _queryExecuter.Query<SpecializationResponse>(" Select Id , Name , Description from Specializations Where IsActive = 1 ");
        if (Response is null)
            return Result.Failure<IEnumerable<SpecializationResponse>>(SpecializationErrors.SpecializationNotFound);
        return Result.Success(Response);
    }
}