namespace Sehaty_Plus.Application.Common.EmailService
{
    public interface IEmailSenderService
    {
        Task SendConfirmationEmailAsync(ApplicationUser user, string code);
        Task SendChangeEmailConfirmationAsync(ApplicationUser user, string newEmail, string code);
        Task SendResetPasswordConfirmationAsync(ApplicationUser user, string code);
        Task SendConfirmEmailCodeAndSetPasswordEmailAsync(ApplicationUser user, string code);
    }
}
