using Mapster;
using Microsoft.AspNetCore.Mvc;
using Sehaty_Plus.Application.Common.SmsService.YourApp.Application.Interfaces.Services;
using System.Security.Cryptography;
using System.Text;
using Twilio.Rest;

namespace Sehaty_Plus.Infrastructure.Services.Sms
{
    public class OtpService : IOtpService
    {
        public (Otp otp, string plainCode) GenerateOtp(string userId)
        {
            var plainCode = GenerateOtpCode();
            var hashedCode = HashOtpCode(plainCode);

            var otp = new Otp
            {
                UserId = userId,
                Code = hashedCode,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(5)
            };

            return (otp, plainCode);
        }

        public bool VerifyOtp(Otp otp, string enteredCode)
        {
            if (otp.ExpiresAt < DateTime.UtcNow)
                return false;

            var enteredHash = HashOtpCode(enteredCode);
            return otp.Code == enteredHash;
        }
        private static string GenerateOtpCode(int length = 6)
        {
            var random = new Random();
            return string.Concat(Enumerable.Range(0, length)
                .Select(_ => random.Next(0, 10).ToString()));
        }
        public string HashOtpCode(string code)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(code);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

    }
}