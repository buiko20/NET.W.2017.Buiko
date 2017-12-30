using System;

namespace BLL.Services.AccountIdService
{
    /// <inheritdoc />
    /// <summary>
    /// The exception thrown by service in case of an error.
    /// </summary>
    public class AccountIdServiceException : Exception
    {
        /// <inheritdoc />
        public AccountIdServiceException() : base()
        {
        }

        /// <inheritdoc />
        public AccountIdServiceException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public AccountIdServiceException(string message, Exception innerException) :
            base(message, innerException)
        {
        }
    }
}
