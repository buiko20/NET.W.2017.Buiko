using System;

namespace Services.Interface.AccountIdService
{
    /// <inheritdoc />
    /// <summary>
    /// The exception thrown by service in case of an error.
    /// </summary>
    public class AccountIdServiceException : Exception
    {
        /// <inheritdoc />
        public AccountIdServiceException()
        {
        }

        /// <inheritdoc />
        public AccountIdServiceException(string message) :
            base(message)
        {
        }

        /// <inheritdoc />
        public AccountIdServiceException(string message, Exception innerException) :
            base(message, innerException)
        {
        }
    }
}
