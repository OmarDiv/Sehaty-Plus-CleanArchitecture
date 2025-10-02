namespace Sehaty_Plus.Application.Feature.Specializations.Command.DeleteSpecialization;

public record DeleteSpecialization(int Id) : IRequest<Result>;

public class DeleteSpecializationHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<DeleteSpecialization, Result>
{
    private readonly IApplicationDbContext _dbContext = applicationDbContext;

    public async Task<Result> Handle(DeleteSpecialization request, CancellationToken cancellationToken)
    {
        if (await _dbContext.Specializations.Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken: cancellationToken) is not { } IsExsist)
            return Result.Failure<bool>(SpecializationErrors.SpecializationNotFound);
        _dbContext.Specializations.Remove(IsExsist);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
