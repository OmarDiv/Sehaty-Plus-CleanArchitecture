namespace Sehaty_Plus.Application.Feature.Specializations.Command.UpdateSpecialization
{
    public class UpdateSpecializationDtoValidator : AbstractValidator<UpdateSpecializationDto>
    {
        public UpdateSpecializationDtoValidator()
        {
            RuleFor(x => x.Name)
                  .NotEmpty().WithMessage("{PropertyName} is required")
                  .MaximumLength(100).WithMessage("{PropertyName} must not be longer than 100 characters.");

            RuleFor(x => x.Description)
                  .MaximumLength(500).WithMessage("{PropertyName} must not be longer than 500 characters.")
                  .When(x => x.Description is not null);
        }
    }
}
