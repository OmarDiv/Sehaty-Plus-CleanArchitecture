using Sehaty_Plus.Domain.Enums;

namespace Sehaty_Plus.Application.Feature.User.Commands.RegisterUser
{
    public record RegisterUser(string Email, string Password, string FirstName, string LastName, Gender Gender) : IRequest<Result>;
    public class RegisterUserHandler(IAuthService _authService) : IRequestHandler<RegisterUser, Result>
    {
        public async Task<Result> Handle(RegisterUser request, CancellationToken cancellationToken)
        {
            return await _authService.RegisterAdminAsync(request, cancellationToken);
        }
    }
}
