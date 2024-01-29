using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit.Text;
using MimeKit;
using Orange_Backend.Services.Interfaces;
using Orange_Backend.Helpers;
using MailKit.Net.Smtp;

namespace Orange_Backend.Services
{
    public class EmailService:IEmailService
    {
        private readonly EmailSettings _emailSettings;

        

        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        public async void Send(string to, string subject, string html,string from=null)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from ?? _emailSettings.From));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html  };

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync(_emailSettings.Server, _emailSettings.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(from ?? _emailSettings.From, _emailSettings.Password);

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
        }
    }
}
