using System;
using System.Collections.Generic;
using System.Linq;
using Logger;
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

        private ILogger _logger;

        #endregion // !private fields.

        #region public

        #region constructors

        /// <summary>
        /// Initializes an instance of BookService with the passed parameter.
        /// </summary>
        /// <param name="bookStorage">book storage implementation.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="bookStorage"/>.</exception>
        public BookService(IBookStorage bookStorage)
        {
            if (ReferenceEquals(bookStorage, null))
            {
                throw new ArgumentNullException(nameof(bookStorage));
            }

            _bookStorage = bookStorage;
            Logger = LoggerFactory.GetNLogger(this.GetType().FullName);

            try
            {
                _books.AddRange(_bookStorage.GetBooks());
            }
            catch (Exception)
            {
                _books.Clear();
            }
        }

        /// <summary>
        /// Initializes an instance of BookService with the passed parameter.
        /// </summary>
        /// <param name="bookStorage">book storage implementation.</param>
        /// <param name="logger">logger</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="bookStorage"/>
        /// or <paramref name="logger"/> is null.</exception>
        public BookService(IBookStorage bookStorage, ILogger logger)
        {
            if (ReferenceEquals(bookStorage, null))
            {
                throw new ArgumentNullException(nameof(bookStorage));
            }

            if (ReferenceEquals(logger, null))
            {
                throw new ArgumentNullException(nameof(logger));
            }

            _bookStorage = bookStorage;
            Logger = logger;

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

        #region properties

        /// <summary>
        /// Logger property injection.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
        public ILogger Logger
        {
            get
            {
                return _logger;
            }

            set
            {
                if (ReferenceEquals(value, null))
                {
                    throw new ArgumentNullException(nameof(Logger));
                }

                _logger = value;
            }
        }

        #endregion // !properties.

        #region interfaces implementation

        /// <inheritdoc />
        public void AddBook(Book book)
        {
            if (ReferenceEquals(book, null))
            {
                Logger.Info($"{nameof(AddBook)}. book is null");
                throw new ArgumentNullException(nameof(book));
            }

            try
            {
                _books.Add(book);
            }
            catch (Exception e)
            {
                Logger.Info(e, "Add book error");
                throw new ServiceException("Add book error", e);
            }
        }

        /// <inheritdoc />
        public void AddBook(params Book[] books)
        {
            if (ReferenceEquals(books, null))
            {
                Logger.Info($"{nameof(AddBook)}. book is null");
                throw new ArgumentNullException(nameof(books));
            }

            try
            {
                foreach (var book in books)
                {
                    if (!ReferenceEquals(book, null))
                    {
                        _books.Add(book);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Info(e, "Add book error");
                throw new ServiceException("Add book error", e);
            }
        }

        /// <inheritdoc />
        public void RemoveBook(Book book)
        {
            if (ReferenceEquals(book, null))
            {
                Logger.Info($"{nameof(RemoveBook)}. book is null");
                throw new ArgumentNullException(nameof(book));
            }

            try
            {
                _books.Remove(book);
            }
            catch (Exception e)
            {
                Logger.Info(e, "Remove book error");
                throw new ServiceException("Remove book error", e);
            }
        }

        /// <inheritdoc />
        public void RemoveBook(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
            {
                Logger.Info($"{nameof(RemoveBook)}. isbn is invalid");
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
                Logger.Info(e, "Remove book error");
                throw new ServiceException("Remove book error", e);
            }
        }

        /// <inheritdoc />
        public void RemoveBook(IPredicate<Book> predicate)
        {
            if (ReferenceEquals(predicate, null))
            {
                Logger.Info($"{nameof(RemoveBook)}. predicate is null");
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
                Logger.Info(e, "Remove book error");
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
                Logger.Info($"{nameof(FindBook)}. predicate is null");
                throw new ArgumentNullException(nameof(predicate));
            }

            return _books.Where(predicate.Choose);
        }

        /// <inheritdoc />
        public IEnumerable<Book> GetBooks() =>
            _books.ToArray();

        /// <inheritdoc />
        public void Sort() => 
            _books.Sort();

        /// <inheritdoc />
        public void Sort(IComparer<Book> comparer)
        {
            if (ReferenceEquals(comparer, null))
            {
                Logger.Info($"{nameof(comparer)}. is null");
                throw new ArgumentNullException(nameof(comparer));
            }

            _books.Sort(comparer);
        }

        /// <inheritdoc />
        public void Save() =>
            _bookStorage.Save(_books);

        #endregion // !interfaces implementation.

        #endregion // !public.
    }
}
