namespace Sehaty_Plus.Application.Feature.Specializations.Queries.GetSpecializationByIdDetailed
{
    public class GetSpecializationByIdDetailedValidator
        : AbstractValidator<GetSpecializationByIdDetailed>
    {
        public GetSpecializationByIdDetailedValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
