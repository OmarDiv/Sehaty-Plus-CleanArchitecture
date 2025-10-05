using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sehaty_Plus.Application.Feature.Auth.Commands.RegisterUser
{
    public record RegisterAdmin(string Email, string Password, string FirstName, string LastName) : IRequest<Result>;
    public class RegisterUserHandler(IAuthService _authService) : IRequestHandler<RegisterAdmin, Result>
    {
        public async Task<Result> Handle(RegisterAdmin request, CancellationToken cancellationToken)
        {
            return await _authService.RegisterUserAsync(request, cancellationToken);
        }
    }
}
