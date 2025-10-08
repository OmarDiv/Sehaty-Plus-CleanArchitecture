namespace Sehaty_Plus.Application.Feature.Auth.Commands.ResendConfirmEmail
{
    public class ResendConfirmEmailValidator : AbstractValidator<ResendConfirmEmail>
    {
        public ResendConfirmEmailValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
