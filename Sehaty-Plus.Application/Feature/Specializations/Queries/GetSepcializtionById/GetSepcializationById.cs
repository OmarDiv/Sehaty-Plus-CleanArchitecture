namespace Sehaty_Plus.Application.Feature.Specializations.Queries.GetSepcializtionById;

public record GetSpecializationById(int Id) : IRequest<Result<SpecializationResponse>>;
public class GetSpecializtionByIdHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetSpecializationById, Result<SpecializationResponse>>
{
    public async Task<Result<SpecializationResponse>> Handle(GetSpecializationById request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Specializations.GetSpecializationByIdAsync(request.Id, cancellationToken);
        if (result is null)
            return Result.Failure<SpecializationResponse>(SpecializationErrors.SpecializationNotFound);
        return Result.Success(result);

    }
}
