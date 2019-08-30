using MailKit.Net.Smtp;
using MimeKit;

namespace MySchool.Web.EmailSending
{
    public class EmailSender
    {
        public void Send(string to, string subject, string text)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("krisikostadinov123@gmail.com"));

            message.To.Add(new MailboxAddress(to));

            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = text
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);

                client.Authenticate("krisikostadinov123@gmail.com", "0882974259k");
                client.Send(message);
            }
        }
    }
}
