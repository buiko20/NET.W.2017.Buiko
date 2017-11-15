using System;
using System.Globalization;
using Logic.Domain;

namespace Logic.NUnitTests
{
    public class CustomBookFormatter : IFormatProvider, ICustomFormatter
    {
        #region private fields

        private const string SupportedFormat = "IAN";

        private readonly IFormatProvider _parentFormatProvider;

        #endregion // !private fields.

        #region public 

        #region constructors

        /// <inheritdoc />
        /// <summary>
        /// Initializes an instance of a class with default parameters.
        /// </summary>
        public CustomBookFormatter() : this(CultureInfo.CurrentCulture)
        {
        }

        /// <summary>
        /// Initializes an instance of a class.
        /// </summary>
        /// <param name="parentFormatProvider">parent format provider</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="parentFormatProvider"/> is null.</exception>
        public CustomBookFormatter(IFormatProvider parentFormatProvider)
        {
            if (ReferenceEquals(parentFormatProvider, null))
            {
                throw new ArgumentNullException(nameof(parentFormatProvider));
            }

            _parentFormatProvider = parentFormatProvider;
        }

        #endregion // !constructors.

        #region interface implementation

        /// <inheritdoc />
        public object GetFormat(Type formatType) =>
            formatType == typeof(ICustomFormatter) ? this : null;

        /// <inheritdoc />
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (string.IsNullOrWhiteSpace(format))
            {
                return HandleOtherFormats(format, arg);
            }

            if (format.Trim().ToUpperInvariant() != SupportedFormat)
            {
                return HandleOtherFormats(format, arg);
            }

            var book = arg as Book;
            if (ReferenceEquals(book, null))
            {
                return HandleOtherFormats(format, arg);
            }

            return $"ISBN 13: {book.Isbn} {book.Author} {book.Name}";
        }

        #endregion //!interface implementation.

        #endregion // !public.

        #region private

        private string HandleOtherFormats(string format, object arg)
        {
            try
            {
                var formattable = arg as IFormattable;
                if (formattable != null)
                {
                    return formattable.ToString(format, _parentFormatProvider);
                }

                return arg?.ToString() ?? string.Empty;
            }
            catch (FormatException e)
            {
                throw new FormatException($"The format of '{format}' is invalid.", e);
            }
        }

        #endregion // !private.
    }
}
