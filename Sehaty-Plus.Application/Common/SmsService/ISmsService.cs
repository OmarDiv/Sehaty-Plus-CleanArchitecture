using Twilio.Rest.Api.V2010.Account;

namespace Sehaty_Plus.Application.Common.SmsService
{
    public interface ISmsService
    {
        Task<Result<MessageResource>> SendAsync(string toPhoneNumber, string Otp, CancellationToken cancellationToken);
    }
}
