namespace Sehaty_Plus.Application.Feature.Specializations.Command.UpdateSpecialization
{
    public class UpdateSpecializationValidator : AbstractValidator<UpdateSpecialization>
    {
        public UpdateSpecializationValidator() 
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();



        }
    }
}
