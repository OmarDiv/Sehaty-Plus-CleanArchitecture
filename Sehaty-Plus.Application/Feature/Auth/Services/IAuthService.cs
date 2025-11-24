using Sehaty_Plus.Application.Feature.Auth.Commands.ConfirmEmail;
using Sehaty_Plus.Application.Feature.Auth.Commands.ConfirmResetPassword;
using Sehaty_Plus.Application.Feature.Auth.Commands.ResendConfirmEmail;
using Sehaty_Plus.Application.Feature.Auth.Commands.VerfiyForgetPasswordOtp;
using Sehaty_Plus.Application.Feature.User.Commands.RegisterDoctor;
using Sehaty_Plus.Application.Feature.User.Commands.RegisterPatient;
using Sehaty_Plus.Application.Feature.User.Commands.RegisterUser;
namespace Sehaty_Plus.Application.Feature.Auth.Services
{
    public interface IAuthService
    {

        Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken);
        Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken);
        Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken);
        Task<Result> RegisterPatientAsync(RegisterPatient request, CancellationToken cancellationToken);
        Task<Result> RegisterDoctorAsync(RegisterDoctor request, CancellationToken cancellationToken);
        Task<Result> RegisterUserAsync(RegisterUser request, CancellationToken cancellationToken);
        Task<Result> ConfirmEmailAsync(ConfirmEmail request, CancellationToken cancellationToken);
        Task<Result> ResendConfirmEmailAsync(ResendConfirmEmail request, CancellationToken cancellationToken);
        Task<Result<string>> SendForgetPasswordOtpAsync(string phoneNumber, CancellationToken cancellationToken);
        Task<Result<string>> VerfiyForgetPasswordOtp(VerifyForgetPasswordOtp request, CancellationToken cancellationToken);
        Task<Result> ConfirmResetPasswordAsync(ConfirmResetPassword request, CancellationToken cancellationToken);
    }
}