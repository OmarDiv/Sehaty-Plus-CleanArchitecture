using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
