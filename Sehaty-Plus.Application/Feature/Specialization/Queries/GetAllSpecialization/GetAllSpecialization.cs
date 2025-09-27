using MediatR;

namespace Sehaty_Plus.Application.Feature.Specialization.Queries.GetAllSpecialization
{
    public record GetAllSpecializationsQuery():IRequest<IEnumerable<SpecializationResponse>>;
}
