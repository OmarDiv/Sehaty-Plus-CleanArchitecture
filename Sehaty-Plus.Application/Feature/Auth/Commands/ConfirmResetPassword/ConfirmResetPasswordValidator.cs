using Sehaty_Plus.Application.Common.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sehaty_Plus.Application.Feature.Auth.Commands.ConfirmResetPassword
{
    public class ConfirmResetPasswordValidator : AbstractValidator<ConfirmResetPassword>
    {
        public ConfirmResetPasswordValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(RegexPatterns.EgyptPhoneNumer)
                .WithMessage("Invalid Egyptian phone number. Format: 01xxxxxxxxx or +2001xxxxxxxxx");
            RuleFor(x => x.newPassword)
                .NotEmpty()
                .Matches(RegexPatterns.Password)
                .WithMessage("Password should be at least 8 digits and should contains Lowercase, NonAlphanumeric and Uppercase");
            RuleFor(x => x.confirmPassword)
                .NotEmpty()
                .Equal(x => x.newPassword)
                .WithMessage("ConfirmPassword do not match your Password");
            RuleFor(x => x.resetToken)
                .NotEmpty();

        }
    }
}
