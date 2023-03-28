using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace BTechHaar.Main.Services
{
    public interface IEmailService
    {
        Task SendEmail(string toEmailId, string subject, string htmlStr);
        Task SendOTPEmail(string toEmailId, string OTPText);
    }

    public class EmailService : IEmailService
    {
        public EmailService() { }

        public async Task SendEmail(string toEmailId, string subject, string htmlStr)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("haarsolutions@outlook.com"));
            email.To.Add(MailboxAddress.Parse(toEmailId));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = htmlStr };

            using var smtp = new SmtpClient();
            //smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            //smtp.Authenticate("haarsolutions", "Bacardi@6583");
            smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("haarsolutions@outlook.com", "Bacardi@6583");
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public async Task SendOTPEmail(string toEmailId, string OTPText)
        {
            try
            {
                var subject = "Haar Solutions - OTP";
                var htmlBody = "<p> Here is your one-time password. Please use the OTP for successfull login <br /> <h2> " + OTPText + "</h2></p>";
                await SendEmail(toEmailId, subject, htmlBody);
            }catch(Exception ex)
            {

            }
        }
    }
}
