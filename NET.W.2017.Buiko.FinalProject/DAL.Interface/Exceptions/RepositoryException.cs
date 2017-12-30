using System;

namespace DAL.Interface.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// Exception that is thrown if an error occurred in the repository.
    /// </summary>
    public class RepositoryException : Exception
    {
        /// <inheritdoc />
        public RepositoryException() : base()
        {
        }

        /// <inheritdoc />
        public RepositoryException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public RepositoryException(string message, Exception innerException) : 
            base(message, innerException)
        {
        }
    }
}
