﻿using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Logic.Domain
{
    /// <summary>
    /// Class representing the book.
    /// </summary>
    public class Book : IComparable, IComparable<Book>, IEquatable<Book>, IFormattable
    {
        #region private fields

        private string _isbn;
        private string _author;
        private string _name;
        private string _publishingHouse;
        private string _publicationYear;
        private int _pageNumber;
        private decimal _price;

        #endregion // !private fields.

        #region public

        #region counstructors

        /// <summary>
        /// Initializes an instance of the book with the passed parameters.
        /// </summary>
        /// <param name="isbn">book isbn</param>
        /// <param name="author">book author</param>
        /// <param name="name">book name</param>
        /// <param name="publishingHouse">book publishing</param>
        /// <param name="publicationYear">year of book publishing</param>
        /// <param name="pageNumber">number of pages in the book</param>
        /// <param name="price">book price</param>
        public Book(string isbn, string author, string name, string publishingHouse, string publicationYear, int pageNumber, decimal price)
        {
            Isbn = isbn;
            Author = author;
            Name = name;
            PublishingHouse = publishingHouse;
            PublicationYear = publicationYear;
            PageNumber = pageNumber;
            Price = price;
        }

        #endregion // !counstructors.

        #region properties

        /// <summary>
        /// International Standard Book Number.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="value"/> invalid.
        /// </exception>
        public string Isbn
        {
            get
            {
                return _isbn;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(nameof(Isbn));
                }

                const string isbn13Pattern = "^(?:ISBN(?:-13)?:? )?(?=[0-9]{13}$|" +
                                             "(?=(?:[0-9]+[- ]){4})[- 0-9]{17}$)97[89]" +
                                             "[- ]?[0-9]{1,5}[- ]?[0-9]+[- ]?[0-9]+[- ]?[0-9]$";
                var regularExpression = new Regex(isbn13Pattern);
                if (!regularExpression.IsMatch(value))
                {
                    throw new ArgumentException("Invalid isbn format", nameof(Isbn));
                }

                _isbn = value;
            }
        }

        /// <summary>
        /// Book author.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="value"/> IsNullOrWhiteSpace.
        /// </exception>
        public string Author
        {
            get
            {
                return _author;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(nameof(Author));
                }

                _author = value;
            }
        }

        /// <summary>
        /// Book name.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="value"/> IsNullOrWhiteSpace.
        /// </exception>
        public string Name
        {
            get
            {
                return _name;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(nameof(Name));
                }

                _name = value;
            }
        }

        /// <summary>
        /// Publishing house.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="value"/> IsNullOrWhiteSpace.
        /// </exception>
        public string PublishingHouse
        {
            get
            {
                return _publishingHouse;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(nameof(PublishingHouse));
                }

                _publishingHouse = value;
            }
        }

        /// <summary>
        /// Publication year.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="value"/> is invalid.
        /// </exception>
        public string PublicationYear
        {
            get
            {
                return _publicationYear;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(nameof(PublicationYear));
                }

                try
                {
                    // Should I check the date range?
                    var date = int.Parse(value);
                }
                catch (FormatException)
                {
                    throw new ArgumentException("Invalid data", nameof(PublicationYear));
                }

                _publicationYear = value;
            }
        }

        /// <summary>
        /// Page number.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="value"/> is less than or equal to 0.
        /// </exception>
        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(nameof(PageNumber));
                }

                _pageNumber = value;
            }
        }

        /// <summary>
        /// Price.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="value"/> is less than or equal to 0.
        /// </exception>
        public decimal Price
        {
            get
            {
                return _price;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(nameof(Price));
                }

                _price = value;
            }
        }

        #endregion // !properties.

        #region object override methods

        /// <summary>
        /// Returns a string representation of a book.
        /// </summary>
        /// <returns>String representation of a book.</returns>
        public override string ToString() =>
            this.ToString("G", null);

        /// <summary>
        /// Verifies the equivalence of the current book and the <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">object to compare</param>
        /// <returns>True if objects are equivalent, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            return obj.GetType() == this.GetType() && this.Equals((Book)obj);
        }

        /// <summary>
        /// Returns book hash code.
        /// </summary>
        /// <returns>Book hash code.</returns>
        public override int GetHashCode() =>
            Isbn.GetHashCode();

        #endregion // !object override methods.

        #region interfaces implementation

        /// <inheritdoc />
        public int CompareTo(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return 1;
            }

            return obj.GetType() != this.GetType() ? 1 : this.CompareTo((Book)obj);
        }

        /// <inheritdoc />
        /// <summary>
        /// Compares two books by name ignoring case.
        /// </summary>
        /// <param name="other">book for comparison.</param>
        /// <returns>Greater than 0, if the current book is larger, 0 if equal, -1 if less.</returns>
        public int CompareTo(Book other) =>
            ReferenceEquals(other, null) ? 1 : 
            string.Compare(this.Name, other.Name, StringComparison.CurrentCulture);

        /// <inheritdoc />
        /// <summary>
        /// Сhecks the equivalence of the current book and the <paramref name="other" /> book.
        /// </summary>
        /// <param name="other">book to compare</param>
        /// <returns>True if objects are equivalent, false otherwise.</returns>
        public bool Equals(Book other)
        {
            if (ReferenceEquals(other, this))
            {
                return true;
            }

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            // Isbn - UNIQUE number of the book edition.
            return other.Isbn == Isbn;
        }

        #endregion // !interfaces implementation.

        #region format

        /// <summary>
        /// Returns a string representation of a book according to the <paramref name="format"/>.
        /// </summary>
        /// <param name="format">string representation format</param>
        /// <returns>Book string representation.</returns>
        /// <exception cref="FormatException">Thrown when <paramref name="format"/> is not supported.</exception>
        public string ToString(string format) =>
            this.ToString(format, null);

        /// <inheritdoc />
        /// <exception cref="FormatException">Thrown when <paramref name="format"/> is not supported.</exception>
        public string ToString(string format, IFormatProvider formatProvider)
        {           
            if (string.IsNullOrWhiteSpace(format))
            {
                format = "G";
            }

            if (ReferenceEquals(formatProvider, null))
            {
                formatProvider = CultureInfo.CurrentCulture;
            }

            switch (format.Trim().ToUpperInvariant())
            {
                case "G":
                    return $"ISBN 13: {Isbn} {Author} {Name} {PublishingHouse} " +
                           $"{PublicationYear.ToString(formatProvider)} " +
                           $"{PageNumber.ToString(formatProvider)} " +
                           $"{string.Format(formatProvider, "{0:C0}", Price)}";
                case "AN":
                    return $"{Author} {Name}";
                case "ANH":
                    return $"{Author} {Name} {PublishingHouse}";
                case "IANHYP":
                    return $"ISBN 13: {Isbn} {Author} {Name} {PublishingHouse} " +
                           $"{PublicationYear.ToString(formatProvider)} " +
                           $"{PageNumber.ToString(formatProvider)}";
                case "ANHY":
                    return $"{Author} {Name} {PublishingHouse} " +
                           $"{PublicationYear.ToString(formatProvider)}";
                default:
                    throw new FormatException($"The {format} format string is not supported.");
            }
        }

        #endregion // !format.

        #endregion // !public.
    }
}
