using MimeKit;
using MailKit.Net.Smtp;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace _0_Framework.Application.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void SendEmail(string title, string emailTemplateName, string destinationName, string destinationAddress)
        {
            var message = new MimeMessage();
            var smtpConfig = _configuration.GetSection("SmtpConfig");

            var from = new MailboxAddress(smtpConfig["SenderDisplayName"], smtpConfig["SenderAddress"]);
            message.From.Add(from);

            var to = new MailboxAddress(destinationName, destinationAddress);
            message.To.Add(to);

            message.Subject = title;
            var bodyBuilder = new BodyBuilder
            {
                //HtmlBody = $"<h1>{messageBody}</h1>",
                HtmlBody = GetEmailBody("test")
            };

            message.Body = bodyBuilder.ToMessageBody();

            var client = new SmtpClient();
            //client.Connect("185.88.152.251", 25, false);
            client.Connect(smtpConfig["Host"], int.Parse(smtpConfig["Port"]), bool.Parse(smtpConfig["EnableSsl"]));
            client.Authenticate(smtpConfig["UserName"], smtpConfig["Password"]);
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }



        private string GetEmailBody(string templateName)
        {
            return File.ReadAllText(string.Format($"EmailTemplate/{templateName}.html"));
        }
    }
}