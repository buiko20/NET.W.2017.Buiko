using System;

namespace BLL.Services.MailService
{
    /// <inheritdoc />
    /// <summary>
    /// Exception that is thrown if an error occurred in the mail service.
    /// </summary>
    public class MailServiceException : Exception
    {
        /// <inheritdoc />
        public MailServiceException()
        {
        }

        /// <inheritdoc />
        public MailServiceException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public MailServiceException(string message, Exception innerException) : 
            base(message, innerException)
        {
        }
    }
}
