using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Company.PL.Helper.MailKitFeature
{
    public class MailService : IMailService
    {
        private readonly IOptions<MailKitSetting> _mailSetting;

        public MailService(IOptions<MailKitSetting> mailSetting)
        {
            _mailSetting = mailSetting;
        }

        public bool SendMail(Email email)
        {
            try
            {
                // Build Message
                var Mail = new MimeMessage();
                Mail.From.Add(new MailboxAddress(_mailSetting.Value.DisplayName, _mailSetting.Value.Email)); // Sender
                Mail.To.Add(MailboxAddress.Parse(email.To)); // Receiver
                Mail.Subject = email.Subject; // Subject

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = email.Body;
                Mail.Body = bodyBuilder.ToMessageBody(); // Body


                // Open Connection With Mail Server
                using var SmtpServerClient = new SmtpClient(); // Server Client
                SmtpServerClient.Connect(_mailSetting.Value.Host, _mailSetting.Value.Port);
                SmtpServerClient.Authenticate(_mailSetting.Value.Email, _mailSetting.Value.Password);

                // Send Mail
                SmtpServerClient.Send(Mail);

                return true ;
            }

            catch (Exception)
            {
                return false;
            }

        }
    }
}
