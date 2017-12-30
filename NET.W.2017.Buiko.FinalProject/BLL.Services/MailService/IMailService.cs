using System;
using System.Threading.Tasks;

namespace BLL.Services.MailService
{
    /// <summary>
    /// Simple mail service.
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// Send mail using <paramref name="mailData"/>.
        /// </summary>
        /// <param name="mailData">mail data</param>
        /// <exception cref="ArgumentNullException">Exception thrown when 
        /// <paramref name="mailData"/> is null.</exception>
        /// <exception cref="MailServiceException">Exception thrown when 
        /// error occurs in the mail service.</exception>
        void SendMail(MailData mailData);

        /// <summary>
        /// Send mail using <paramref name="mailData"/>.
        /// </summary>
        /// <param name="mailData">mail data</param>
        /// <exception cref="ArgumentNullException">Exception thrown when 
        /// <paramref name="mailData"/> is null.</exception>
        /// <exception cref="MailServiceException">Exception thrown when 
        /// error occurs in the mail service.</exception>
        /// <returns>Task.</returns>
        Task SendMailAsync(MailData mailData);
    }
}
