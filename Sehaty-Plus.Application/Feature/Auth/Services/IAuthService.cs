using Sehaty_Plus.Application.Feature.Auth.Commands.ConfirmEmail;
using Sehaty_Plus.Application.Feature.Auth.Commands.RegisterAdmin;
using Sehaty_Plus.Application.Feature.Auth.Commands.RegisterDoctor;
using Sehaty_Plus.Application.Feature.Auth.Commands.RegisterPatient;
using Sehaty_Plus.Application.Feature.Auth.Commands.ResendConfirmEmail;
namespace Sehaty_Plus.Application.Feature.Auth.Services
{
    public interface IAuthService
    {

        Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default);
        Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
        Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
        Task<Result> RegisterPatientAsync(RegisterPatient request, CancellationToken cancellationToken);
        Task<Result> RegisterDoctorAsync(RegisterDoctor request, CancellationToken cancellationToken);
        Task<Result> RegisterAdminAsync(RegisterAdmin request, CancellationToken cancellationToken);

        Task<Result> ConfirmEmailAsync(ConfirmEmail request, CancellationToken cancellationToken);
        Task<Result> ResendConfirmEmailAsync(ResendConfirmEmail request, CancellationToken cancellationToken);
        //Task<Result> SendResetPasswordCodeAsync(string email);
        //Task<Result> ConfirmResetPasswordAsync(ResetPasswordRequest request);
    }
}