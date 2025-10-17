using Humanizer;
using System.Net;
using System.Net.Mail;

namespace Company.PL.Helper.MailKitFeature
{
    public static class EmailProcessing
    {
        public static bool SendMail(Email email)
        {
            try
            {
                var MailServerClient = new SmtpClient("smtp.gmail.com", 587);
                MailServerClient.EnableSsl = true;
                MailServerClient.Credentials = new NetworkCredential("seif27168@gmail.com",
                                                                     "rgnhehgfwhocbrhi");
                MailServerClient.Send(
                    from: "seif27168@gmail.com",
                    recipients: email.To,
                    subject: email.Subject,
                    body: email.Body
                    );
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error sending email: {e.Message}");
                return false;
            }

        }

    }
}
