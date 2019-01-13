using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Data.Repositories.Identity
{
    public class EmailService : IIdentityMessageService
    {
        private string emailAdress = "email@bk.ru";
        private string emailPassword = "password";

        public Task SendAsync(IdentityMessage message)
        {
            return Task.Factory.StartNew(() => 
            {
                sendMail(message);
            });
        }

        void sendMail(IdentityMessage message)
        {
            //#region formatter
            //string text = string.Format("Please click on this link to {0}: {1}", message.Subject, message.Body);
            //string html = "Please confirm your account by clicking this link: <a href=\"" + message.Body + "\">link</a><br/>";

            //html += HttpUtility.HtmlEncode(@"Or click on the copy the following link on the browser:" + message.Body);
            //#endregion

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(emailAdress);
            msg.To.Add(new MailAddress(message.Destination));
            msg.Subject = message.Subject;
            msg.Body = message.Body;
            msg.IsBodyHtml = true;
            //msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            //msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            SmtpClient smtpClient = new SmtpClient("smtp.bk.ru", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(emailAdress, emailPassword);
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;
            smtpClient.Send(msg);
        }
    }
}
