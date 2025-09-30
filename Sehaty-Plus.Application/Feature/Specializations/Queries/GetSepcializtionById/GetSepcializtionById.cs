using Sehaty_Plus.Application.Feature.Specializations.Errors;
using Sehaty_Plus.Application.Feature.Specializations.Responses;

namespace Sehaty_Plus.Application.Feature.Specializations.Queries.GetSepcializtionById;

public record GetSpecializationsById(int Id) : IRequest<Result<SpecializationResponse>>;
public class GetSpecializtionByIdHandler(IQueryExecuter queryExecuter) : IRequestHandler<GetSpecializationsById, Result<SpecializationResponse>>
{
    public async Task<Result<SpecializationResponse>> Handle(GetSpecializationsById request, CancellationToken cancellationToken)
    {
        var result = await queryExecuter.QueryFirstOrDefault<SpecializationResponse>("SELECT * FROM Specializations WHERE Id = @id", new { request.Id });
        if (result is null)
            return Result.Failure<SpecializationResponse>(SpecializationErrors.SpecializationNotFound);
        return Result.Success(result);

    }
}
