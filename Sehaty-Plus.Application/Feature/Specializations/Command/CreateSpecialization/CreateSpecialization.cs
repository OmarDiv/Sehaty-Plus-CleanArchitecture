using Microsoft.EntityFrameworkCore;
using Sehaty_Plus.Application.Common.Interfaces;
namespace Sehaty_Plus.Application.Feature.Specializations.Command.CreateSpecialization;

public record CreateSpecialization(string Name, string Description) : IRequest<Result<SpecializationResponse>>;

public class CreateSpecializationHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<CreateSpecialization, Result<SpecializationResponse>>
{
    private readonly IApplicationDbContext _dbcontext = applicationDbContext;

    public async Task<Result<SpecializationResponse>> Handle(CreateSpecialization request, CancellationToken cancellationToken)
    {
        var specializationExists = await _dbcontext.Specializations
            .AnyAsync(s => s.Name == request.Name, cancellationToken);
        if (specializationExists)
            return Result.Failure<SpecializationResponse>(SpecializationErrors.SpecializationDuplicate);
        var entity = request.Adapt<Specialization>();
        await _dbcontext.Specializations.AddAsync(entity, cancellationToken);
        await _dbcontext.SaveChangesAsync(cancellationToken);
        return Result.Success(entity.Adapt<SpecializationResponse>());
    }
}
