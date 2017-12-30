using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BLL.Services.MailService;

namespace BLL.Services
{
    /// <inheritdoc />
    public class GmailService : IMailService
    {
        /// <inheritdoc />
        public void SendMail(MailData mailData)
        {
            if (ReferenceEquals(mailData, null))
            {
                throw new ArgumentNullException(nameof(mailData));
            }

            try
            {
                var data = ConfigureData(mailData);

                var email = data.Item1;
                var smtp = data.Item2;

                smtp.Send(email);
            }
            catch (Exception e)
            {
                throw new MailServiceException("Send mail error.", e);
            }
        }

        /// <inheritdoc />
        public Task SendMailAsync(MailData mailData)
        {
            if (ReferenceEquals(mailData, null))
            {
                throw new ArgumentNullException(nameof(mailData));
            }

            try
            {
                var data = ConfigureData(mailData);

                var email = data.Item1;
                var smtp = data.Item2;

                return smtp.SendMailAsync(email);
            }
            catch (Exception e)
            {
                throw new MailServiceException("Send mail error.", e);
            }
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
