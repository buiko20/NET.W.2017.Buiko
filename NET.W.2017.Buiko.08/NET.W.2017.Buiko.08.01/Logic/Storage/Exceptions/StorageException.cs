using System;

namespace Logic.Storage.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// The exception thrown by storage in case of an error.
    /// </summary>
    public class StorageException : Exception
    {
        /// <inheritdoc />
        public StorageException()
        {
        }

        /// <inheritdoc />
        public StorageException(string message) :
            base(message)
        {
        }

        /// <inheritdoc />
        public StorageException(string message, Exception innerException) :
            base(message, innerException)
        {
        }
    }
}
