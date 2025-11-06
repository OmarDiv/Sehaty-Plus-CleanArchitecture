namespace Sehaty_Plus.Application.Feature.Specializations.Command.DeleteSpecialization;

public record DeleteSpecialization(int Id) : IRequest<Result>;

public class DeleteSpecializationHandler(IUnitOfWork _unitOfWork) : IRequestHandler<DeleteSpecialization, Result>
{

    public async Task<Result> Handle(DeleteSpecialization request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Specializations.GetByIdAsync(request.Id) is not { } IsExsist)
            return Result.Failure<bool>(SpecializationErrors.SpecializationNotFound);
        await _unitOfWork.Specializations.DeleteAsync(IsExsist);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
