using System;

namespace Logic.AccountCreationService.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// The exception thrown by service in case of an error.
    /// </summary>
    public class AccountCreationServiceException : Exception
    {
        /// <inheritdoc />
        public AccountCreationServiceException()
        {
        }

        /// <inheritdoc />
        public AccountCreationServiceException(string message) : 
            base(message)
        {
        }

        /// <inheritdoc />
        public AccountCreationServiceException(string message, Exception innerException) : 
            base(message, innerException)
        {
        }
    }
}
