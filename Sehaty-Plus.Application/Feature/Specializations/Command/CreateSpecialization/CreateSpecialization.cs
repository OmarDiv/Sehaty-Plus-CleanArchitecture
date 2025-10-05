namespace Sehaty_Plus.Application.Feature.Specializations.Command.CreateSpecialization;

public record CreateSpecialization(string Name, string Description) : IRequest<Result<SpecializationDetailedResponse>>;

public class CreateSpecializationHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<CreateSpecialization, Result<SpecializationDetailedResponse>>
{
    private readonly IApplicationDbContext _dbcontext = applicationDbContext;

    public async Task<Result<SpecializationDetailedResponse>> Handle(CreateSpecialization request, CancellationToken cancellationToken)
    {
        var specializationExists = await _dbcontext.Specializations
            .AnyAsync(s => s.Name == request.Name, cancellationToken);
        if (specializationExists)
            return Result.Failure<SpecializationDetailedResponse>(SpecializationErrors.SpecializationDuplicate);
        var entity = request.Adapt<Specialization>();
        await _dbcontext.Specializations.AddAsync(entity, cancellationToken);
        await _dbcontext.SaveChangesAsync(cancellationToken);
        return Result.Success(entity.Adapt<SpecializationDetailedResponse>());
    }
}
