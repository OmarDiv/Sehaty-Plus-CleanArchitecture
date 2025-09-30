namespace Sehaty_Plus.Application.Feature.Specializations.Command.CreateSpecialization
{
    public class CreateSpecializationValidtor : AbstractValidator<CreateSpecialization>
    {
        public CreateSpecializationValidtor()
        {

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();
        }
    }
}
