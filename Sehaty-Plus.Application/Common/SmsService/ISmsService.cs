using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Verify.V2.Service;

namespace Sehaty_Plus.Application.Common.SmsService
{
    public interface ISmsService
    {
        Task<Result<MessageResource>> SendAsync(string toPhoneNumber, string Otp, CancellationToken cancellationToken);
    }
}
