namespace Sehaty_Plus.Application.Feature.Auth.Commands.ConfirmResetPassword
{
    public record ConfirmResetPassword(string PhoneNumber, string newPassword, string confirmPassword, string resetToken) : IRequest<Result>;
    public class ResetPasswordHandler(IAuthService _authService) : IRequestHandler<ConfirmResetPassword, Result>
    {
        public async Task<Result> Handle(ConfirmResetPassword request, CancellationToken cancellationToken)
        {
            return await _authService.ConfirmResetPasswordAsync(request, cancellationToken);
        }
    }

}

