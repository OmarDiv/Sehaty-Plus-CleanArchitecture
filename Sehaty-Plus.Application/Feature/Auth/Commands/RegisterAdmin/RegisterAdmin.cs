using Sehaty_Plus.Domain.Enums;

namespace Sehaty_Plus.Application.Feature.Auth.Commands.RegisterAdmin
{
    public record RegisterAdmin(string Email, string Password, string FirstName, string LastName, Gender Gender) : IRequest<Result>;
    public class RegisterUserHandler(IAuthService _authService) : IRequestHandler<RegisterAdmin, Result>
    {
        public async Task<Result> Handle(RegisterAdmin request, CancellationToken cancellationToken)
        {
            return await _authService.RegisterAdminAsync(request, cancellationToken);
        }
    }
}
