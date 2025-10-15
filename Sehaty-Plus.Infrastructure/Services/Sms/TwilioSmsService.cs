using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sehaty_Plus.Application.Common.SmsService;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Sehaty_Plus.Infrastructure.Services.Sms
{
    public class TwilioSmsService(IOptions<TwilioSettings> _settings, ILogger<TwilioSmsService> _logger) : ISmsService
    {
        public async Task<Result<MessageResource>> SendAsync(string phoneNumber, string otp, CancellationToken cancellationToken = default)
        {
            try
            {
                TwilioClient.Init(_settings.Value.AccountSID, _settings.Value.AuthToken);

                var message = $"Your Sehaty Plus OTP code is: {otp}. Valid for 5 minutes.";
                var formattedNumber = phoneNumber.StartsWith("+2") ? phoneNumber : $"+2{phoneNumber}";

                var messageResource = await MessageResource.CreateAsync(
                    body: message,
                    from: new PhoneNumber(_settings.Value.TwilioPhoneNumber),
                    to: formattedNumber
                );
                _logger.LogInformation($"OTP sent successfully to {phoneNumber}. SID: {messageResource.Sid}");
                return Result.Success(messageResource);
            }
            catch (Exception)
            {
                _logger.LogError($"Failed to send OTP to {phoneNumber}");
                return Result.Failure<MessageResource>(new("Twilio Error", "Sending Wrong", StatusCode: StatusCodes.Status500InternalServerError));
            }
        }
    }
}