// Services/EmailService.cs
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Threading.Tasks;

namespace Insurance_agency.Services.SendMail
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var smtpSettings = _configuration.GetRequiredSection("MailKitSMTP");
            var smtpServer = smtpSettings["Host"]
                ?? throw new InvalidOperationException("Missing required configuration 'MailKitSMTP:Host'.");
            var smtpPort = smtpSettings.GetValue<int>("Port");
            var smtpUser = smtpSettings["UserName"]
                ?? throw new InvalidOperationException("Missing required configuration 'MailKitSMTP:UserName'.");
            var smtpPassword = smtpSettings["Password"]
                ?? throw new InvalidOperationException("Missing required configuration 'MailKitSMTP:Password'.");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Your App Name", smtpUser));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(smtpServer, smtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(smtpUser, smtpPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
