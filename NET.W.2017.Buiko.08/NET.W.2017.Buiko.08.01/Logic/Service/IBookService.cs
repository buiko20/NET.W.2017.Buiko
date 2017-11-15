using System.Collections.Generic;
using Logic.Domain;
using Logic.Service.Exceptions;

namespace Logic.Service
{
    /// <summary>
    /// Interface describing service contract.
    /// </summary>
    public interface IBookService
    {
        /// <summary>
        /// Adds a <paramref name="book"/> to the storage.
        /// </summary>
        /// <param name="book">the book, to be added to the storage.</param>
        /// <exception cref="ServiceException">
        /// Thrown when an error occurred in service.
        /// </exception>
        void AddBook(Book book);

        /// <summary>
        /// Add <paramref name="books"/> to the storage.
        /// </summary>
        /// <exception cref="ServiceException">
        /// Thrown when an error occurred in service.
        /// </exception>
        void AddBook(params Book[] books);

        /// <summary>
        /// Removes a specific <paramref name="book"/> from storage.
        /// </summary>
        /// <param name="book">book to remove</param>
        /// <exception cref="ServiceException">
        /// Thrown when an error occurred in service.
        /// </exception>
        void RemoveBook(Book book);

        /// <summary>
        /// Removes book with specific <paramref name="isbn"/> from storage.
        /// </summary>
        /// <param name="isbn">book isbn</param>
        /// <exception cref="ServiceException">
        /// Thrown when an error occurred in service.
        /// </exception>
        void RemoveBook(string isbn);

        /// <summary>
        /// Removes all books that match the specified <paramref name="predicate"/> from storage.
        /// </summary>
        /// <param name="predicate">predicate selecting books from all books in the storage</param>
        /// <exception cref="ServiceException">
        /// Thrown when an error occurred in service.
        /// </exception>
        void RemoveBook(IPredicate<Book> predicate);

        /// <summary>
        /// Looks for a book by the specified <paramref name="isbn"/> in the storage.
        /// </summary>
        /// <param name="isbn">book isbn</param>
        /// <returns>A book or null if there is such book in the storage.</returns>
        /// <exception cref="ServiceException">
        /// Thrown when an error occurred in service.
        /// </exception>
        Book FindBook(string isbn);

        /// <summary>
        /// Looks for books on a given <paramref name="predicate"/> in the storage.
        /// </summary>
        /// <param name="predicate">the criterion for choosing books</param>
        /// <returns>Found books.</returns>
        /// <exception cref="ServiceException">
        /// Thrown when an error occurred in service.
        /// </exception>
        IEnumerable<Book> FindBook(IPredicate<Book> predicate);

        /// <summary>
        /// Returns all books stored in the storage.
        /// </summary>
        /// <returns>All books contained in the storage.</returns>
        /// <exception cref="ServiceException">
        /// Thrown when an error occurred in service.
        /// </exception>
        IEnumerable<Book> GetBooks();

        /// <summary>
        /// Sorts all books in the storage using book IComparable.
        /// </summary>
        /// <exception cref="ServiceException">
        /// Thrown when an error occurred in service.
        /// </exception>
        void Sort();

        /// <summary>
        /// Sorts all books in the storage using <paramref name="comparer"/>.
        /// </summary>
        /// <param name="comparer">criterion for sorting books.</param>
        /// <exception cref="ServiceException">
        /// Thrown when an error occurred in service.
        /// </exception>
        void Sort(IComparer<Book> comparer);

        /// <summary>
        /// Save changes.
        /// </summary>
        /// <exception cref="ServiceException">
        /// Thrown when an error occurred in service.
        /// </exception>
        void Save();
    }
}
