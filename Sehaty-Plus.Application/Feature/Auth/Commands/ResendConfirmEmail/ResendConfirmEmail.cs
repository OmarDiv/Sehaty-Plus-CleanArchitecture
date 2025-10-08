namespace Sehaty_Plus.Application.Feature.Auth.Commands.ResendConfirmEmail
{
    public record ResendConfirmEmail(string Email) : IRequest<Result>;
    public class ResendConfirmEmailHandler(IAuthService authService) : IRequestHandler<ResendConfirmEmail, Result>
    {
        public async Task<Result> Handle(ResendConfirmEmail request, CancellationToken cancellationToken)
        {
            return await authService.ResendConfirmEmailAsync(request, cancellationToken);
        }
    }
}
