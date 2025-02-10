using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using UserManagementAPI.Helpers;

namespace UserManagementAPI.Services
{
    public class EmailService
    {
        private readonly MailSettings _mailSettings;

        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var fromAddress = new MailAddress(_mailSettings.Mail, _mailSettings.DisplayName);
                var toAddress = new MailAddress(toEmail);
                using (var smtp = new SmtpClient())
                {
                    smtp.Host = _mailSettings.Host;
                    smtp.Port = _mailSettings.Port;
                    smtp.EnableSsl = _mailSettings.EnableSSL;
                    smtp.Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password);

                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    })
                    {
                        await smtp.SendMailAsync(message); // Send email asynchronously
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error here or rethrow
                throw new InvalidOperationException("Could not send email", ex);
            }
        }
    }
}
