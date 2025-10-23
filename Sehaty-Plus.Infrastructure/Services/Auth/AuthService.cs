﻿using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sehaty_Plus.Application.Common.Authentication;
using Sehaty_Plus.Application.Common.EmailService;
using Sehaty_Plus.Application.Common.Interfaces;
using Sehaty_Plus.Application.Common.SmsService;
using Sehaty_Plus.Application.Common.SmsService.YourApp.Application.Interfaces.Services;
using Sehaty_Plus.Application.Feature.Auth.Commands.ConfirmEmail;
using Sehaty_Plus.Application.Feature.Auth.Commands.ConfirmResetPassword;
using Sehaty_Plus.Application.Feature.Auth.Commands.RegisterAdmin;
using Sehaty_Plus.Application.Feature.Auth.Commands.RegisterDoctor;
using Sehaty_Plus.Application.Feature.Auth.Commands.RegisterPatient;
using Sehaty_Plus.Application.Feature.Auth.Commands.ResendConfirmEmail;
using Sehaty_Plus.Application.Feature.Auth.Commands.VerfiyForgetPasswordOtp;
using Sehaty_Plus.Application.Feature.Auth.Errors;
using Sehaty_Plus.Application.Feature.Auth.Responses;
using Sehaty_Plus.Application.Feature.Auth.Services;
using Sehaty_Plus.Application.Feature.Patients.Errors;
using Sehaty_Plus.Domain.Enums;
using Sehaty_Plus.Infrastructure.Persistence;
using System.Security.Cryptography;
using System.Text;

namespace Sehaty_Plus.Infrastructure.Services.Auth
{
    public class AuthService(
        UserManager<ApplicationUser> _userManager,
        IApplicationDbContext _context,
        IJwtProvider _jwtProvider,
        IEmailSenderService _emailSenderService,
        SignInManager<ApplicationUser> _signInManager,
        ILogger<AuthService> _logger,
        IOtpService _otpService
        ) : IAuthService
    {
        private static readonly int _refreshTokenExpiryDays = 14;
        public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            if (await _userManager.FindByEmailAsync(email) is not { } user)
                return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            if (result.Succeeded)
            {
                (string token, int expiresIn) = _jwtProvider.GenerateToken(user);
                var refreshToken = GenerateRefreshToken();
                var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);
                user.RefreshTokens.Add(new RefreshToken
                {
                    Token = refreshToken,
                    ExpiresOn = refreshTokenExpiration,
                });
                var isUpdated = await _userManager.UpdateAsync(user);
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
            return Result.Failure<AuthResponse>(result.IsNotAllowed ? UserErrors.EmailNotConfirmed : UserErrors.InvalidCredentials);
        }
        public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            if (_jwtProvider.ValidateToken(token) is not string userId)
                return Result.Failure<AuthResponse>(UserErrors.InvalidUserOrRefershToken);
            if (await _userManager.FindByIdAsync(userId) is not { } user)
                return Result.Failure<AuthResponse>(UserErrors.InvalidUserOrRefershToken);
            if (user.RefreshTokens.SingleOrDefault(rt => rt.Token == refreshToken && rt.IsActive) is not { } oldRefreshToken)
                return Result.Failure<AuthResponse>(UserErrors.InvalidUserOrRefershToken);
            oldRefreshToken.RevokedOn = DateTime.UtcNow;
            (string newToken, int expiresIn) = _jwtProvider.GenerateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);
            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                ExpiresOn = refreshTokenExpiration,
            });
            var isUpdated = await _userManager.UpdateAsync(user);
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
            if (_jwtProvider.ValidateToken(token) is not string userId)
                return Result.Failure(UserErrors.InvalidUserOrRefershToken);
            if (await _userManager.FindByIdAsync(userId) is not { } user)
                return Result.Failure(UserErrors.InvalidUserOrRefershToken);
            if (user.RefreshTokens.SingleOrDefault(rt => rt.Token == refreshToken && rt.IsActive) is not { } oldRefreshToken)
                return Result.Failure(UserErrors.InvalidUserOrRefershToken);
            oldRefreshToken.RevokedOn = DateTime.UtcNow;
            var isUpdated = await _userManager.UpdateAsync(user);
            if (!isUpdated.Succeeded)
                return Result.Failure(UserErrors.FailedToUpdateUser);
            return Result.Success();
        }

        public async Task<Result> RegisterPatientAsync(RegisterPatient request, CancellationToken cancellationToken)
        {
            if ((await _userManager.Users.AnyAsync(x => x.Email == request.Email)))
                return Result.Failure(UserErrors.DuplicatedEmail);
            if (await _context.Patients.AnyAsync(x => x.NationalId == request.NationalId))
                return Result.Failure(PateintErrors.DuplicatedNationalId);
            if (request.Password != request.ConfirmPassword)
                return Result.Failure(UserErrors.MissMatchPassword);
            if (await _userManager.Users.AnyAsync(x => x.PhoneNumber == request.PhoneNumber))
                return Result.Failure(UserErrors.DuplicatedPhoneNumber);
            if (request.NationalId.Length != 14 || !request.NationalId.All(char.IsDigit))
                return Result.Failure(PateintErrors.InvalidNationalId);
            var dbContext = (ApplicationDbContext)_context;
            var strategy = dbContext.Database.CreateExecutionStrategy();
            var result = await strategy.ExecuteAsync(async () =>
             {
                 await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
                 try
                 {
                     var user = new ApplicationUser
                     {
                         Email = request.Email,
                         UserName = request.Email,
                         FirstName = request.FirstName,
                         LastName = request.LastName,
                         PhoneNumber = request.PhoneNumber,
                         Gender = request.Gender,
                     };

                     var result = await _userManager.CreateAsync(user, request.Password);

                     if (!result.Succeeded)
                     {
                         await transaction.RollbackAsync(cancellationToken);
                         return Result.Failure(UserErrors.InvalidCredentials);

                     }
                     var patient = new Patient
                     {
                         UserId = user.Id,
                         NationalId = request.NationalId,
                         DateOfBirth = request.DateOfBirth,
                         BloodType = request.BloodType,
                         EmergencyContact = request.EmergencyContact,
                         Allergies = request.Allergies
                     };

                     await _context.Patients.AddAsync(patient);
                     await _context.SaveChangesAsync(cancellationToken);

                     await transaction.CommitAsync(cancellationToken);
                     return Result.Success();
                 }
                 catch (Exception)
                 {
                     await transaction.RollbackAsync(cancellationToken);
                     return Result.Failure(UserErrors.InvalidCredentials);
                 }
             });
            return result;
        }

        public async Task<Result> RegisterDoctorAsync(RegisterDoctor request, CancellationToken cancellationToken)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == request.Email || x.PhoneNumber == request.PhoneNumber, cancellationToken))
                return Result.Failure(UserErrors.InvalidCredentials);

            if (request.Password != request.ConfirmPassword)
                return Result.Failure(UserErrors.MissMatchPassword);

            if (await _context.Doctors.AnyAsync(x => x.LicenseNumber == request.LicenseNumber, cancellationToken))
                return Result.Failure(UserErrors.InvalidCredentials);

            var dbContext = (ApplicationDbContext)_context;
            var strategy = dbContext.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    var user = new ApplicationUser
                    {
                        Email = request.Email,
                        UserName = request.Email,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        PhoneNumber = request.PhoneNumber,
                        Gender = request.Gender,
                    };

                    var createResult = await _userManager.CreateAsync(user, request.Password);

                    if (!createResult.Succeeded)
                    {
                        var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                        throw new Exception($"User creation failed: {errors}");
                    }

                    var doctor = new Doctor
                    {
                        UserId = user.Id,
                        LicenseNumber = request.LicenseNumber,
                        YearsOfExperience = request.YearsOfExperience,
                        Biography = request.Biography,
                        Education = request.Education,
                        IsVerified = false
                    };

                    await dbContext.Doctors.AddAsync(doctor, cancellationToken);
                    await dbContext.SaveChangesAsync(cancellationToken);

                    await transaction.CommitAsync(cancellationToken);

                    return Result.Success();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return Result.Failure(UserErrors.InvalidCredentials);
                }
            });
        }

        public async Task<Result> RegisterAdminAsync(RegisterAdmin request, CancellationToken cancellationToken)
        {
            if ((await _userManager.Users.AnyAsync(x => x.Email == request.Email)))
                return Result.Failure(UserErrors.DuplicatedEmail);

            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                _logger.LogInformation("Admin registered with Code: {code}", code);
                await _emailSenderService.SendConfirmationEmailAsync(user, code);
                return Result.Success();
            }
            var error = result.Errors.First();
            return Result.Failure(new(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        public async Task<Result> ConfirmEmailAsync(ConfirmEmail request, CancellationToken cancellationToken)
        {
            if (await _userManager.FindByIdAsync(request.UserId) is not { } user)
                return Result.Failure(UserErrors.InvaildCode);
            if (user.EmailConfirmed)
                return Result.Failure(UserErrors.DuplicatedConfirmation);
            var code = string.Empty;
            try
            {
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));

            }
            catch (FormatException)
            {
                return Result.Failure(UserErrors.InvaildCode);
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return Result.Success();
            var error = result.Errors.First();
            return Result.Failure(new(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        public async Task<Result> ResendConfirmEmailAsync(ResendConfirmEmail request, CancellationToken cancellationToken)
        {
            if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
                return Result.Success();
            if (user.EmailConfirmed)
                return Result.Failure(UserErrors.DuplicatedConfirmation);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            _logger.LogInformation("Email confirmation Code: {code}", code);
            await _emailSenderService.SendConfirmationEmailAsync(user, code);
            return Result.Success();
        }
        public async Task<Result<string>> SendForgetPasswordOtpAsync(string phoneNumber, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .Include(u => u.Otps)
                .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber, cancellationToken);

            if (user is null)
                return Result.Success("OTP sent successfully Check Your Phone");

            var lastOtp = user.Otps.OrderByDescending(x => x.CreatedAt).FirstOrDefault();
            if (lastOtp != null && lastOtp.CreatedAt.AddMinutes(1) > DateTime.UtcNow)
                return Result.Failure<string>(
                    new("TooManyRequests", "You can request a new OTP after 1 minute.", StatusCodes.Status429TooManyRequests)
                );

            var (otp, otpCode) = _otpService.GenerateOtp(user.Id);

            user.Otps.Add(otp);
            if (lastOtp is not null)
                lastOtp.ExpiresAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
            BackgroundJob.Enqueue<ISmsService>(sms => sms.SendAsync(phoneNumber, otpCode, CancellationToken.None));
            return Result.Success("OTP sent successfully Check Your Phone");
        }
        public async Task<Result<string>> VerfiyForgetPasswordOtp(VerifyForgetPasswordOtp request, CancellationToken cancellationToken)
        {
            if (await _userManager.Users.Include(u => u.Otps).FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber, cancellationToken) is not { } user)
                return Result.Failure<string>(UserErrors.UserNotFound);
            var entryOtp = _otpService.HashOtpCode(request.OtpCode);
            var lastOtp = user.Otps.LastOrDefault();
            if (lastOtp is null || lastOtp.Code != entryOtp || lastOtp.ExpiresAt < DateTime.UtcNow)
                return Result.Failure<string>(UserErrors.InvalidOrExpiredOtp);
            lastOtp.ExpiresAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(resetToken));
            return Result.Success(encodedToken);
        }
        public async Task<Result> ConfirmResetPasswordAsync(ConfirmResetPassword request, CancellationToken cancellationToken)
        {
            if (request.newPassword != request.confirmPassword)
                return Result.Failure(UserErrors.MissMatchPassword);
            if (await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber, cancellationToken) is not { } user)
                return Result.Failure(UserErrors.UserNotFound);
            var decodedToken = string.Empty;
            try
            {
                decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.resetToken));
            }
            catch (FormatException)
            {
                return Result.Failure(UserErrors.InvalidCredentials);
            }
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, request.newPassword);
            if (result.Succeeded)
                return Result.Success();
            var error = result.Errors.First();
            return Result.Failure(new(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }
    }
}
