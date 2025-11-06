using Hangfire;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Sehaty_Plus.Application.Common.EmailService;

namespace Sehaty_Plus.Infrastructure.Services.Email
{
    public class EmailSenderService(
        IHttpContextAccessor httpContextAccessor,
        IOptions<MailSettings> options) : IEmailSenderService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly MailSettings _mailSettings = options.Value;

        public async Task SendConfirmationEmailAsync(ApplicationUser user, string code)
        {
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin ?? "https://sehaty-plus.com";

            var emailBody = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation", new Dictionary<string, string>
                     {
                           { "{{FirstName}}", user.FirstName },
                           { "{{ConfirmLink}}", $"{origin}/auth/confirm-email?userId={user.Id}&code={code}" },
                           { "{{Code}}", code }
                     });

            BackgroundJob.Enqueue(() => SendEmailAsync(
                user.Email!,
                "✅ Sehaty Plus: Confirm Your Email",
                emailBody
            ));

            await Task.CompletedTask;
        }
        public async Task SendChangeEmailOtpAsync(ApplicationUser user, string otpCode)
        {
            var emailBody = EmailBodyBuilder.GenerateEmailBody("EmailChangeOtp", new Dictionary<string, string>
                {
                            { "{{FirstName}}", user.FirstName },
                            { "{{OtpCode}}", otpCode }
                });

            BackgroundJob.Enqueue(() => SendEmailAsync(user.Email!, "🔐 Sehaty Plus: Verify Your New Email", emailBody));

            await Task.CompletedTask;
        }
        public async Task SendResetPasswordConfirmationAsync(ApplicationUser user, string code)
        {
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

            var emailBody = EmailBodyBuilder.GenerateEmailBody("ResetPasswordConfirmation", new Dictionary<string, string>
                                {
                                    { "{{UserName}}", user.FirstName },
                                    { "{{ResetLink}}", $"{origin}/auth/reset-password?userId={user.Id}&code={code}" }
                                });

            BackgroundJob.Enqueue(() => SendEmailAsync(user.Email!, "✅Sehaty-Plus:Reset Your Password", emailBody));

            await Task.CompletedTask;
        }
        public async Task SendConfirmEmailCodeAndSetPasswordEmailAsync(ApplicationUser user, string code)
        {
            var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

            var emailBody = EmailBodyBuilder.GenerateEmailBody("WelcomeAndSetPassword", new Dictionary<string, string>
                {
                    { "{{UserName}}", user.FirstName },
                    { "{{SetPasswordLink}}", $"{origin}/auth/set-password?userId={user.Id}&code={code}" }
                });

            BackgroundJob.Enqueue(() => SendEmailAsync(user.Email!, "🔐Sehaty-Plus: Welcome! Set Your Password", emailBody));

            await Task.CompletedTask;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Mail),
                Subject = subject
            };

            message.To.Add(MailboxAddress.Parse(email));

            var builder = new BodyBuilder
            {
                HtmlBody = htmlMessage
            };

            message.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();


            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(message);
            smtp.Disconnect(true);
        }

    }
}

