namespace Sehaty_Plus.Application.Feature.Specializations.Queries.GetSpecializationByIdDetailed
{
    public record GetSpecializationByIdDetailed(int Id) : IRequest<Result<SpecializationDetailedResponse>>;
    public class GetSpecializationByIdDetailedHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetSpecializationByIdDetailed, Result<SpecializationDetailedResponse>>
    {
        public async Task<Result<SpecializationDetailedResponse>> Handle(GetSpecializationByIdDetailed request, CancellationToken cancellationToken)
        {
            var Response = await _unitOfWork.Specializations.GetSpecializationByIdDetailedAsync(request.Id, cancellationToken);
            if (Response is null)
                return Result.Failure<SpecializationDetailedResponse>(SpecializationErrors.SpecializationNotFound);
            return Result.Success(Response);
        }
    }
}
