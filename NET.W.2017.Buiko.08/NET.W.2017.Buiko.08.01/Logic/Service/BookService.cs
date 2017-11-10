using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Domain;
using Logic.Service.Exceptions;
using Logic.Storage;

namespace Logic.Service
{
    /// <inheritdoc />
    public class BookService : IBookService
    {
        #region private fields

        private readonly IBookStorage _bookStorage;
        private readonly List<Book> _books = new List<Book>();

        #endregion // !private fields.

        #region public

        #region constructors

        /// <summary>
        /// Initializes an instance of BookService with the passed parameter.
        /// </summary>
        /// <param name="bookStorage">Book storage implementation.</param>
        public BookService(IBookStorage bookStorage)
        {
            if (ReferenceEquals(bookStorage, null))
            {
                throw new ArgumentNullException(nameof(bookStorage));
            }

            _bookStorage = bookStorage;

            try
            {
                _books.AddRange(_bookStorage.GetBooks());
            }
            catch (Exception)
            {
                _books.Clear();
            }
        }

        #endregion // !constructors.

        #region interfaces implementation

        /// <inheritdoc />
        public void AddBook(Book book)
        {
            if (ReferenceEquals(book, null))
            {
                throw new ArgumentNullException(nameof(book));
            }

            try
            {
                _books.Add(book);
            }
            catch (Exception e)
            {
                throw new ServiceException("Add book error", e);
            }
        }

        /// <inheritdoc />
        public void AddBook(params Book[] books)
        {
            if (ReferenceEquals(books, null))
            {
                throw new ArgumentNullException(nameof(books));
            }

            try
            {
                foreach (var book in books)
                {
                    _books.Add(book);
                }
            }
            catch (Exception e)
            {
                throw new ServiceException("Add book error", e);
            }
        }

        /// <inheritdoc />
        public void RemoveBook(Book book)
        {
            if (ReferenceEquals(book, null))
            {
                throw new ArgumentNullException(nameof(book));
            }

            try
            {
                _books.Remove(book);
            }
            catch (Exception e)
            {
                throw new ServiceException("Remove book error", e);
            }
        }

        /// <inheritdoc />
        public void RemoveBook(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
            {
                throw new ArgumentException(nameof(isbn));
            }

            try
            {
                for (int i = 0; i < _books.Count; i++)
                {
                    if (_books[i].Isbn == isbn)
                    {
                        _books.RemoveAt(i);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                throw new ServiceException("Remove book error", e);
            }
        }

        /// <inheritdoc />
        public void RemoveBook(IPredicate<Book> predicate)
        {
            if (ReferenceEquals(predicate, null))
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            try
            {
                for (int i = 0; i < _books.Count; i++)
                {
                    if (predicate.Choose(_books[i]))
                    {
                        _books.RemoveAt(i);
                    }
                }
            }
            catch (Exception e)
            {
                throw new ServiceException("Remove book error", e);
            }
        }

        /// <inheritdoc />
        public Book FindBook(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
            {
                throw new ArgumentException(nameof(isbn));
            }

            return _books.FirstOrDefault(book => book.Isbn == isbn);
        }

        /// <inheritdoc />
        public IEnumerable<Book> FindBook(IPredicate<Book> predicate)
        {
            if (ReferenceEquals(predicate, null))
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return _books.Where(predicate.Choose);
        }

        /// <inheritdoc />
        public IEnumerable<Book> GetBooks() =>
            _books.ToArray();

        /// <inheritdoc />
        public void Sort() => 
            _Sort(null);

        /// <inheritdoc />
        public void Sort(IComparer<Book> comparator) => 
            _Sort(comparator);

        /// <inheritdoc />
        public void Save() =>
            _bookStorage.Save(_books);

        #endregion // !interfaces implementation.

        #endregion // !public.

        #region private

        private void _Sort(IComparer<Book> comparator)
        {
            var books = _books.ToArray();

            if (ReferenceEquals(comparator, null))
            {
                Array.Sort(books);
            }
            else
            {
                Array.Sort(books, comparator);
            }

            _books.Clear();
            _books.AddRange(books);
        }

        #endregion // !private.
    }
}
