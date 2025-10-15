using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sehaty_Plus.Application.Common.SmsService
{
    public class TwilioSettings
    {
        public static string SectionName = "Twilio";
        public string AccountSID { get; set; } = string.Empty;
        public string AuthToken { get; set; } = string.Empty;
        public string TwilioPhoneNumber { get; set; } = string.Empty;
    }   
}
