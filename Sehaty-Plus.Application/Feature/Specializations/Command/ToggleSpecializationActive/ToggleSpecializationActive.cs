namespace Sehaty_Plus.Application.Feature.Specializations.Command.ToggleSpecializationActive;

public record ToggleSpecializationActive(int Id) : IRequest<Result>;


public class ToggleActiveHandler(IUnitOfWork _unitOfWork) : IRequestHandler<ToggleSpecializationActive, Result>
{

    public async Task<Result> Handle(ToggleSpecializationActive request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Specializations.GetByIdAsync(request.Id, cancellationToken) is not { } specialization)
            return Result.Failure(SpecializationErrors.SpecializationNotFound);
        specialization.IsActive = !specialization.IsActive;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
