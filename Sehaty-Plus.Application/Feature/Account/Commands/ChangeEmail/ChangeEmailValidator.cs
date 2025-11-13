namespace Sehaty_Plus.Application.Feature.Account.Commands.ChangeEmail
{
    public class ChangeEmailValidator : AbstractValidator<ChangeEmail>
    {
        public ChangeEmailValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

        }
    }
}
