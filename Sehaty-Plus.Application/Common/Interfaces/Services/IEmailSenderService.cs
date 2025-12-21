namespace Sehaty_Plus.Application.Common.Interfaces.Services
{
    public interface IEmailSenderService
    {
        Task SendConfirmationEmailAsync(ApplicationUser user, string code);
        Task SendChangeEmailOtpAsync(ApplicationUser user, string code);
        Task SendResetPasswordConfirmationAsync(ApplicationUser user, string code);
        Task SendConfirmEmailCodeAndSetPasswordEmailAsync(ApplicationUser user, string code);
    }
}
