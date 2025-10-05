namespace Sehaty_Plus.Application.Feature.Specializations.Command.UpdateSpecialization
{
    public record UpdateSpecialization(int Id, string Name, string? Description) : IRequest<Result>;

    public class UpdateSpecializationHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<UpdateSpecialization, Result>
    {
        private readonly IApplicationDbContext _dbContext = applicationDbContext;
        public async Task<Result> Handle(UpdateSpecialization request, CancellationToken cancellationToken)
        {
            if (await _dbContext.Specializations.AnyAsync(s => s.Name == request.Name && s.Id != request.Id, cancellationToken))
                return Result.Failure(SpecializationErrors.SpecializationDuplicate);

            if (await _dbContext.Specializations.FindAsync(request.Id, cancellationToken) is not { } specialization)
                return Result.Failure(SpecializationErrors.SpecializationNotFound);
            request.Adapt(specialization);
            _dbContext.Specializations.Update(specialization);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
