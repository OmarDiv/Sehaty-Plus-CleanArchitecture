using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sehaty_Plus.Application.Feature.Auth.Commands.RevokeRefreshToken
{
    public class RevokeRereshTokenValidator : AbstractValidator<RevokeRefreshToken>
    {
        public RevokeRereshTokenValidator()
        {
            RuleFor(x => x.Token)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.RefreshToken)
                .NotNull()
                .NotEmpty();
        }
    }
}
