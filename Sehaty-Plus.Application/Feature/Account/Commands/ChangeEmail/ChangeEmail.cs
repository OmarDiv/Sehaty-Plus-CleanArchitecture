using Microsoft.AspNetCore.Identity;
using Sehaty_Plus.Application.Common.EmailService;
using Sehaty_Plus.Application.Common.SmsService.YourApp.Application.Interfaces.Services;

namespace Sehaty_Plus.Application.Feature.Account.Commands.ChangeEmail
{
    public record ChangeEmail(string UserId) : IRequest<Result>;
    public class ChagneEmailHandler(UserManager<ApplicationUser> _userManager, IEmailSenderService _emailSenderService, IOtpService _otpService) : IRequestHandler<ChangeEmail, Result>
    {
        public async Task<Result> Handle(ChangeEmail request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
              .Include(u => u.Otps)
              .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

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
            await _emailSenderService.SendChangeEmailOtpAsync(user, otpCode);
            return Result.Success("OTP sent successfully Check Your Phone");
        }
    }
}
