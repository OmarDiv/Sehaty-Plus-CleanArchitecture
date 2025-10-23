using Sehaty_Plus.Application.Common.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sehaty_Plus.Application.Feature.Account.Commands.ChangePassword
{
    public class ChangePassswordValidator : AbstractValidator<ChangePassword>
    {
        public ChangePassswordValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage("Old password is required.");
            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .Matches(RegexPatterns.Password)
                .WithMessage("Password should be at least 8 digits and should contains Lowercase, NonAlphanumeric and Uppercase");
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .Equal(x => x.NewPassword).WithMessage("Confirm password must match the new password.");

        }
    }
}
