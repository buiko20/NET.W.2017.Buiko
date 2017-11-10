using System.Collections.Generic;
using Logic.Domain;
using Logic.Storage.Exceptions;

namespace Logic.Storage
{
    /// <summary>
    /// Interface describing storage contract.
    /// </summary>
    public interface IBookStorage
    {
        /// <summary>
        /// Returns all books stored in the storage.
        /// </summary>
        /// <returns>All books contained in the storage.</returns>
        /// <exception cref="StorageException">
        /// Thrown when an error occurred in dao.
        /// </exception>
        IEnumerable<Book> GetBooks();

        /// <summary>
        /// Saves changes.
        /// </summary>
        /// <param name="books">books to save</param>
        /// <exception cref="StorageException">
        /// Thrown when an error occurred in dao.
        /// </exception>
        void Save(IEnumerable<Book> books);
    }
}
