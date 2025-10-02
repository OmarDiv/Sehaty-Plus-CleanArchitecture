﻿namespace Sehaty_Plus.Application.Feature.Auth.Services
{
    public interface IAuthService
    {

        Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default);
        Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
        Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
        //Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);
        //Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request);
        //Task<Result> ResendConfirmEmailAsync(ResendConfirmtionEmailRequest request, CancellationToken cancellationToken);
        //Task<Result> SendResetPasswordCodeAsync(string email);
        //Task<Result> ConfirmResetPasswordAsync(ResetPasswordRequest request);


    }
}