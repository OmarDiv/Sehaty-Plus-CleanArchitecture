using Sehaty_Plus.Application.Common.Const;

namespace Sehaty_Plus.Application.Feature.Auth.Commands.SendForgetPasswordOtp
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPassword>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(x => x.PhoneNumber)
                 .NotEmpty()
                 .Matches(RegexPatterns.EgyptPhoneNumer)
                 .WithMessage("Invalid Egyptian phone number. Format: 01xxxxxxxxx or +2001xxxxxxxxx");
        }
    }
}
