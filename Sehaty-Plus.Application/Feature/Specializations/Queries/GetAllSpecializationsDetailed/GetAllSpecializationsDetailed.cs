namespace Sehaty_Plus.Application.Feature.Specializations.Queries.GetAllSpecializationsDetailed
{
    public record GetAllSpecializationsDetailed() : IRequest<Result<IEnumerable<SpecializationDetailedResponse>>>;
    public class GetAllSpecializationsDetailedHandler(IQueryExecuter queryExecuter) : IRequestHandler<GetAllSpecializationsDetailed, Result<IEnumerable<SpecializationDetailedResponse>>>
    {
        public async Task<Result<IEnumerable<SpecializationDetailedResponse>>> Handle(GetAllSpecializationsDetailed request, CancellationToken cancellationToken)
        {
            var Response = await queryExecuter.Query<SpecializationDetailedResponse>(" Select * from Specializations ");
            if (Response is null)
                return Result.Failure<IEnumerable<SpecializationDetailedResponse>>(SpecializationErrors.SpecializationNotFound);
            return Result.Success(Response);
        }
    }
}
