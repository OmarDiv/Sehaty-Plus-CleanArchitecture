using Sehaty_Plus.Application.Feature.Auth.Services;

namespace Sehaty_Plus.Application.Feature.Auth.Commands.Login
{
    public record LoginUser(string Email, string Password) : IRequest<Result<AuthResponse>>;
    public class LoginCommandHandler(IAuthService authService) : IRequestHandler<LoginUser, Result<AuthResponse>>
    {
        public async Task<Result<AuthResponse>> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            return await authService.GetTokenAsync(request.Email, request.Password, cancellationToken);
        }
    }
}
