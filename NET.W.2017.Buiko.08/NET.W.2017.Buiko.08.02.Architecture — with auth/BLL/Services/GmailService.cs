using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BLL.Interface.MailService;

namespace BLL.Services
{
    public class GmailService : IMailService
    {
        public void SendMail(MailData mailData)
        {
            var data = ConfigureData(mailData);

            var email = data.Item1;
            var smtp = data.Item2;

            smtp.Send(email);
        }

        public Task SendMailAsync(MailData mailData)
        {
            var data = ConfigureData(mailData);

            var email = data.Item1;
            var smtp = data.Item2;

            return smtp.SendMailAsync(email);
        }

        private static Tuple<MailMessage, SmtpClient> ConfigureData(MailData mailData)
        {
            var from = new MailAddress(mailData.From);
            var to = new MailAddress(mailData.To);

            var email = new MailMessage(from, to)
            {
                Subject = mailData.Subject,
                Body = mailData.Message,
                IsBodyHtml = true
            };

            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(mailData.From, mailData.FromPassword),
                EnableSsl = true
            };

            return Tuple.Create(email, smtp);
        }
    }
}
