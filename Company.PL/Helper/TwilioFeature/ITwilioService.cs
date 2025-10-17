using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML.Voice;

namespace Company.PL.Helper.TwilioFeature
{
    public interface ITwilioService
    {
        public MessageResource SendSms(SMS sms);

    }
}
