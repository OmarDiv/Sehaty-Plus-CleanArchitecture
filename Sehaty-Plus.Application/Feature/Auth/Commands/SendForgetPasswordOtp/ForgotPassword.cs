namespace Sehaty_Plus.Application.Feature.Auth.Commands.SendForgetPasswordOtp
{
    public record ForgotPassword(string PhoneNumber) : IRequest<Result<string>>;
    public class ForgotPasswordHandler(IAuthService _authService) : IRequestHandler<ForgotPassword, Result<string>>
    {
        public async Task<Result<string>> Handle(ForgotPassword request, CancellationToken cancellationToken)
        {
            return await _authService.SendForgetPasswordOtpAsync(request.PhoneNumber, cancellationToken);
        }
    }

}
