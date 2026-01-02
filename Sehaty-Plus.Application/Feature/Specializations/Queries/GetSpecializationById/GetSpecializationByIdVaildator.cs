namespace Sehaty_Plus.Application.Feature.Specializations.Queries.GetSpecializationById
{
    public class GetSpecializationByIdValidator
        : AbstractValidator<GetSpecializationById>
    {
        public GetSpecializationByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
