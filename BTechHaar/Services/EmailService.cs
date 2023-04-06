using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using BTechHaar.Main.Helpers;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace BTechHaar.Main.Services
{
    public interface IEmailService
    {
        Task<string> SendEmail(string toEmailId, string subject, string htmlStr);
        Task<string> SendOTPEmail(string toEmailId, string OTPText);
    }

    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        public EmailService(IOptions<EmailSettings> appEmailSettings)
        {
            _emailSettings = appEmailSettings.Value;
        }

        public async Task<string> SendEmail(string toEmailId, string subject, string htmlStr)
        {
            try
            {
                
                MailMessage msgs = new MailMessage();
                msgs.To.Add(toEmailId);
                MailAddress address = new MailAddress(_emailSettings.fromEmail);
                msgs.From = address;
                msgs.Subject = subject;
                msgs.Body = htmlStr;
                msgs.IsBodyHtml = true;

                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.Host = "relay-hosting.secureserver.net";
                client.Port = 25;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(_emailSettings.fromEmail, _emailSettings.password);
                //Send the msgs  
                client.Send(msgs);
                return string.Empty;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> SendOTPEmail(string toEmailId, string OTPText)
        {
            try
            {
                var subject = "Haar Solutions - OTP";
                var htmlBody = "<p> Here is your one-time password. Please use the OTP for successfull login <br /> <h2> " + OTPText + "</h2></p>";
                return await SendEmail(toEmailId, subject, htmlBody);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
