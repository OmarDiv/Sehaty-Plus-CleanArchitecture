namespace Sehaty_Plus.Application.Common.SmsService
{

    namespace YourApp.Application.Interfaces.Services
    {
        public interface IOtpService
        {
            (Otp otp, string plainCode) GenerateOtp(string userId);
            string HashOtpCode(string code);
            bool VerifyOtp(Otp otp, string enteredCode);
        }
    }
}
