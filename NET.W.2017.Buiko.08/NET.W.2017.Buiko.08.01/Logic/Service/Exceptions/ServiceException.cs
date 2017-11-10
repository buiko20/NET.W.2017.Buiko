using System;

namespace Logic.Service.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// The exception thrown by service in case of an error.
    /// </summary>
    public class ServiceException : Exception
    {
        /// <inheritdoc />
        public ServiceException()
        {
        }

        /// <inheritdoc />
        public ServiceException(string message) :
            base(message)
        {
        }

        /// <inheritdoc />
        public ServiceException(string message, Exception innerException) :
            base(message, innerException)
        {
        }
    }
}
