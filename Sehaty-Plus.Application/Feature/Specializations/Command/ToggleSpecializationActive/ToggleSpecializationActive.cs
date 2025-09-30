namespace Sehaty_Plus.Application.Feature.Specializations.Command.ToggleSpecializationActive;
public record ToggleSpecializationActive(int Id) : IRequest<Result>;


public class ToggleActiveHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<ToggleSpecializationActive, Result>
{
    private readonly IApplicationDbContext _dbContext = applicationDbContext;

    public async Task<Result> Handle(ToggleSpecializationActive request, CancellationToken cancellationToken)
    {
        if (await _dbContext.Specializations.FindAsync(request.Id, cancellationToken) is not { } specialization)
            return Result.Failure(SpecializationErrors.SpecializationNotFound);

        specialization.IsActive = !specialization.IsActive;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Result.Success();

    }
}
