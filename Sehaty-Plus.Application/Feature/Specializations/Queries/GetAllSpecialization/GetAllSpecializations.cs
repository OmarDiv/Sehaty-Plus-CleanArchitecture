namespace Sehaty_Plus.Application.Feature.Specializations.Queries.GetAllSpecialization;

public record GetAllSpecializations() : IRequest<Result<IEnumerable<SpecializationResponse>>>;

public class GetAllSpecializationsHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetAllSpecializations, Result<IEnumerable<SpecializationResponse>>>
{
    public async Task<Result<IEnumerable<SpecializationResponse>>> Handle(GetAllSpecializations request, CancellationToken cancellationToken)
    {
        var Response = await _unitOfWork.Specializations.GetAllActiveAsync(cancellationToken);
        if (Response is null)
            return Result.Failure<IEnumerable<SpecializationResponse>>(SpecializationErrors.SpecializationNotFound);
        return Result.Success(Response);
    }
}