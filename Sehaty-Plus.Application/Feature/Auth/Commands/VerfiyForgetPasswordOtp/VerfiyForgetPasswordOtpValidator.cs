using Sehaty_Plus.Application.Common.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sehaty_Plus.Application.Feature.Auth.Commands.VerfiyForgetPasswordOtp
{
    public class VerfiyForgetPasswordOtpValidator : AbstractValidator<VerfiyForgetPasswordOtp>
    {
        public VerfiyForgetPasswordOtpValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(RegexPatterns.EgyptPhoneNumer)
                .WithMessage("Invalid Egyptian phone number. Format: 01xxxxxxxxx or +2001xxxxxxxxx");
            RuleFor(x => x.OtpCode)
                .NotEmpty()
                .Length(6)
                .WithMessage("OTP code must be 6 characters long.");

        }
    }
}
