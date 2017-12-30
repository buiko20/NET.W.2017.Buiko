using System;

namespace BLL.Interface.Services.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// Exception that is thrown if an error occurred in the bank service.
    /// </summary>
    public class BankServiceException : Exception
    {
        /// <inheritdoc />
        public BankServiceException() : base()
        {
        }

        /// <inheritdoc />
        public BankServiceException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public BankServiceException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
