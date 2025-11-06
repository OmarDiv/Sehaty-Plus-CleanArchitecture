namespace Sehaty_Plus.Application.Feature.Specializations.Command.CreateSpecialization;

public record CreateSpecialization(string Name, string Description) : IRequest<Result<SpecializationDetailedResponse>>;

public class CreateSpecializationHandler(IUnitOfWork _unitOfWork) : IRequestHandler<CreateSpecialization, Result<SpecializationDetailedResponse>>
{

    public async Task<Result<SpecializationDetailedResponse>> Handle(CreateSpecialization request, CancellationToken cancellationToken)
    {

        var specializationExists = await _unitOfWork.Specializations.ExistsByNameAsync(request.Name, null, cancellationToken);
        if (specializationExists)
            return Result.Failure<SpecializationDetailedResponse>(SpecializationErrors.SpecializationDuplicate);
        var entity = request.Adapt<Specialization>();
        await _unitOfWork.Specializations.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(entity.Adapt<SpecializationDetailedResponse>());
    }
}
