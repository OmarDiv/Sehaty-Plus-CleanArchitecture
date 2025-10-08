namespace Sehaty_Plus.Application.Feature.Auth.Commands.ConfirmEmail
{
    public record ConfirmEmail(string UserId, string Code) : IRequest<Result>;
    public class ConfirmEmailHandler(IAuthService _authService) : IRequestHandler<ConfirmEmail, Result>
    {
        public async Task<Result> Handle(ConfirmEmail request, CancellationToken cancellationToken)
        {
            return await _authService.ConfirmEmailAsync(request, cancellationToken);
        }
    }
}
