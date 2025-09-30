
namespace Sehaty_Plus.Application.Feature.Specializations.Command.ToggleSpecializationActive
{
    public class ToggleSpecializationActiveValidtor : AbstractValidator<ToggleSpecializationActive>
    {
        public ToggleSpecializationActiveValidtor()
        {

            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
        }
    }
}
