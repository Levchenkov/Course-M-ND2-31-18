using System.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Lab4
{
    internal class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            SmtpClient client = new SmtpClient("smtp.yandex.ru", 25);
            //SmtpClient client = new SmtpClient("lotus.asb.by", 25);

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(
                ConfigurationManager.AppSettings["mailAccount"],
                ConfigurationManager.AppSettings["mailPassword"]
                );
            client.EnableSsl = true;

            var mail = new MailMessage(
                ConfigurationManager.AppSettings["mailAccount"],
                message.Destination,
                message.Subject,
                message.Body);
            mail.IsBodyHtml = true;

            return client.SendMailAsync(mail);
            //return Task.FromResult(0);
        }
    }
}