namespace Sehaty_Plus.Application.Feature.Specializations.Command.DeleteSpecialization
{
    public class DeleteSpecializationValidtor : AbstractValidator<DeleteSpecialization>
    {
        public DeleteSpecializationValidtor()
        {

            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
        }
    }
}
