namespace Sehaty_Plus.Application.Feature.Specializations.Queries.GetSepcializtionById;

public record GetSpecializationById(int Id) : IRequest<Result<SpecializationResponse>>;
public class GetSpecializtionByIdHandler(IQueryExecuter queryExecuter) : IRequestHandler<GetSpecializationById, Result<SpecializationResponse>>
{
    public async Task<Result<SpecializationResponse>> Handle(GetSpecializationById request, CancellationToken cancellationToken)
    {
        var result = await queryExecuter.QueryFirstOrDefault<SpecializationResponse>("select Id,Name,Description, IsActive from Specializations where Id = @Id", new { request.Id });
        if (result is null)
            return Result.Failure<SpecializationResponse>(SpecializationErrors.SpecializationNotFound);
        return Result.Success(result);

    }
}
