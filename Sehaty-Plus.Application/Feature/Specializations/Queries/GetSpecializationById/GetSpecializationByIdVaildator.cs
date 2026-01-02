namespace Sehaty_Plus.Application.Feature.Specializations.Queries.GetSepcializtionById
{
    public class GetSpecializationByIdValidator
        : AbstractValidator<GetSpecializationByIdDetailed>
    {
        public GetSpecializationByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
