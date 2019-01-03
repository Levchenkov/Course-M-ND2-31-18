using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Web.Models
{
    public static class EmailSender
    {
        public static string SendMail(string mail, string userName)
        {
            MailAddress fromMailAddress = new MailAddress("aspmvclab4@gmail.com", "aspmvclab4");
            MailAddress toMailAddress = new MailAddress(mail, userName);

            string password = Guid.NewGuid().ToString("d").Substring(1, 8);

            using (MailMessage mailMessage = new MailMessage(fromMailAddress, toMailAddress))
            {
                using (SmtpClient smtpClient = new SmtpClient())
                {
                    mailMessage.Subject = "Register on Lab4";
                    mailMessage.Body = $"Your Login: {userName} {Environment.NewLine}Your password: {password}";

                    smtpClient.Host = "smtp.gmail.com";
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(fromMailAddress.Address, "lab4546369364");

                    smtpClient.Send(mailMessage);
                }
            }
            return password;
        }
    }
}
