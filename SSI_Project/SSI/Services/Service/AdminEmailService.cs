using Microsoft.Extensions.Options;
using System.Net.Mail;
using MimeKit;
using MailKit.Net.Smtp;

namespace SSI.Services.Service
{
    public class AdminEmailService
    {
        private readonly IConfiguration _configuration;
        public AdminEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string toEmail, string subject, string body)
        {
            var senderName = _configuration.GetValue<string>("AdminStmpSettings:SenderName");
            var email = _configuration.GetValue<string>("AdminStmpSettings:SenderEmail");
            var pw = _configuration.GetValue<string>("AdminStmpSettings:Password");
            var server = _configuration.GetValue<string>("AdminStmpSettings:Server");
            var port = _configuration.GetValue<int>("AdminStmpSettings:Port");
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(senderName, email));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;
            message.Body = new TextPart("html")
            {
                Text = body
            };
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(server, port, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate(email, pw);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
