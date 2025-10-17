using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Company.PL.Helper.TwilioFeature
{
    public class TwilioService : ITwilioService
    {
        private readonly IOptions<TwilioSetting> _twilioSetting;

        public TwilioService(IOptions<TwilioSetting> twilioSetting)
        {
            _twilioSetting = twilioSetting;
        }

        public MessageResource SendSms(SMS sms)
        {
            try
            {
                TwilioClient.Init(_twilioSetting.Value.AccountSID, _twilioSetting.Value.AuthToken);

                var Sms = MessageResource.Create(
                    to: sms.To,
                    body: sms.Body,
                    from: new Twilio.Types.PhoneNumber(_twilioSetting.Value.PhoneNumber)
                    );

                return Sms;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
