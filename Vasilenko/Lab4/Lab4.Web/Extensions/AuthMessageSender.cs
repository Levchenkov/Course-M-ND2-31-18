using Lab4.Web.Services;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;

namespace Lab4.Web.Extensions
{
    public class AuthMessageSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Admin", ConfigurationManager.AppSetting["AppConfiguration:Email"]));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
            
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync(ConfigurationManager.AppSetting["AppConfiguration:Email"],
                    ConfigurationManager.AppSetting["AppConfiguration:Password"]);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
    
}
