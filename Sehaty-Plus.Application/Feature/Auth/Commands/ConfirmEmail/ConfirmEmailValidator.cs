namespace Sehaty_Plus.Application.Feature.Auth.Commands.ConfirmEmail
{
    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmail>
    {
        public ConfirmEmailValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();
            RuleFor(x => x.Code)
                .NotEmpty();
        }
    }
}
