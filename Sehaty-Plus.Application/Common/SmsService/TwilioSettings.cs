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
