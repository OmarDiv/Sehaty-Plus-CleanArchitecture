namespace Sehaty_Plus.Application.Feature.Specializations.Command.UpdateSpecialization
{
    public record UpdateSpecialization(int Id, string Name, string? Description) : IRequest<Result>;

    public class UpdateSpecializationHandler(IUnitOfWork _unitOfWork) : IRequestHandler<UpdateSpecialization, Result>
    {
        public async Task<Result> Handle(UpdateSpecialization request, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Specializations.ExistsByNameAsync(request.Name, request.Id, cancellationToken))
                return Result.Failure(SpecializationErrors.SpecializationDuplicate);

            if (await _unitOfWork.Specializations.GetByIdAsync(request.Id, cancellationToken) is not { } specialization)
                return Result.Failure(SpecializationErrors.SpecializationNotFound);
            request.Adapt(specialization);
            await _unitOfWork.Specializations.UpdateAsync(specialization);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
