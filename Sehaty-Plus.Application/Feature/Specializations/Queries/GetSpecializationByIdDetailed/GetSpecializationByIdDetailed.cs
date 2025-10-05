namespace Sehaty_Plus.Application.Feature.Specializations.Queries.GetSpecializationByIdDetailed
{
    public record GetSpecializationByIdDetailed(int Id) : IRequest<Result<SpecializationDetailedResponse>>;
    public class GetSpecializationByIdDetailedHandler(IQueryExecuter queryExecuter) : IRequestHandler<GetSpecializationByIdDetailed, Result<SpecializationDetailedResponse>>
    {
        public async Task<Result<SpecializationDetailedResponse>> Handle(GetSpecializationByIdDetailed request, CancellationToken cancellationToken)
        {
            var Response = await queryExecuter.QueryFirstOrDefault<SpecializationDetailedResponse>(" Select * from Specializations where Id = @Id ", new { request.Id });
            if (Response is null)
                return Result.Failure<SpecializationDetailedResponse>(SpecializationErrors.SpecializationNotFound);
            return Result.Success(Response);
        }
    }
}
