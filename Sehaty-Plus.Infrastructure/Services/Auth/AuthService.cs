using Microsoft.AspNetCore.Identity;
using Sehaty_Plus.Application.Common.Authentication;
using Sehaty_Plus.Application.Feature.Auth.Errors;
using Sehaty_Plus.Application.Feature.Auth.Responses;
using Sehaty_Plus.Application.Feature.Auth.Services;
using Sehaty_Plus.Domain.Entities;
using System.Security.Cryptography;

namespace Sehaty_Plus.Infrastructure.Services.Auth
{
    public class AuthService(
        UserManager<ApplicationUser> userManager,
        IJwtProvider jwtProvider
        ) : IAuthService
    {
        private static readonly int _refreshTokenExpiryDays = 14;
        public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            if (await userManager.FindByEmailAsync(email) is not { } user)
                return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);
            if (!await userManager.CheckPasswordAsync(user, password))
                return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);
            (string token, int expiresIn) = jwtProvider.GenerateToken(user);
            var refreshToken = GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);
            user.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                ExpiresOn = refreshTokenExpiration,
            });
            var isUpdated = await userManager.UpdateAsync(user);
            if (!isUpdated.Succeeded)
                return Result.Failure<AuthResponse>(UserErrors.FailedToUpdateUser);
            return Result.Success(new AuthResponse(
                user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                token,
                expiresIn,
                refreshToken,
               refreshTokenExpiration));
        }
        public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            if(jwtProvider.ValidateToken(token) is not string userId)
                return Result.Failure<AuthResponse>(UserErrors.InvalidUserOrRefershToken);
            if (await userManager.FindByIdAsync(userId) is not { } user)
                return Result.Failure<AuthResponse>(UserErrors.InvalidUserOrRefershToken);
            if(user.RefreshTokens.SingleOrDefault(rt => rt.Token == refreshToken && rt.IsActive) is not { } oldRefreshToken)
                return Result.Failure<AuthResponse>(UserErrors.InvalidUserOrRefershToken);
            oldRefreshToken.RevokedOn = DateTime.UtcNow;
            (string newToken, int expiresIn)  = jwtProvider.GenerateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays); 
            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                ExpiresOn = refreshTokenExpiration,
            });
            var isUpdated = await userManager.UpdateAsync(user);
            if (!isUpdated.Succeeded)
                return Result.Failure<AuthResponse>(UserErrors.FailedToUpdateUser);
            return Result.Success(new AuthResponse(
                user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                newToken,
                expiresIn,
                newRefreshToken,
               refreshTokenExpiration));

        }
        private static string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        public async Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            if(jwtProvider.ValidateToken(token) is not string userId)
                return Result.Failure(UserErrors.InvalidUserOrRefershToken);
            if (await userManager.FindByIdAsync(userId) is not { } user)
                return Result.Failure(UserErrors.InvalidUserOrRefershToken);
            if (user.RefreshTokens.SingleOrDefault(rt => rt.Token == refreshToken && rt.IsActive) is not { } oldRefreshToken)
                return Result.Failure(UserErrors.InvalidUserOrRefershToken);
            oldRefreshToken.RevokedOn = DateTime.UtcNow;
            var isUpdated = await userManager.UpdateAsync(user);
            if (!isUpdated.Succeeded)
                return Result.Failure(UserErrors.FailedToUpdateUser);
            return Result.Success();
        }
    }

}
