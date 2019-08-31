using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace MySchool.Web.EmailSending
{
    public class EmailSender
    {
        private readonly IConfiguration configuration;

        public EmailSender(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Send(string to, string subject, string text)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(configuration["EmailSender:EmailFrom"]));

            message.To.Add(new MailboxAddress(to));

            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = text
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);

                client.Authenticate(configuration["EmailSender:EmailFrom"], configuration["EmailSender:Password"]);
                client.Send(message);
            }
        }
    }
}
