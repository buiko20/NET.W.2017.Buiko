using System;

namespace BLL.Services.Account_Service
{
    /// <inheritdoc />
    /// <summary>
    /// The exception thrown by service in case of an error.
    /// </summary>
    public class AccountServiceException : Exception
    {
        /// <inheritdoc />
        public AccountServiceException()
        {
        }

        /// <inheritdoc />
        public AccountServiceException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public AccountServiceException(string message, Exception innerException) : 
            base(message, innerException)
        {
        }
    }
}
