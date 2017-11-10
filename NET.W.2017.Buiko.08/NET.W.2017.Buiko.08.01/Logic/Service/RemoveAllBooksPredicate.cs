using Logic.Domain;

namespace Logic.Service
{
    /// <inheritdoc />
    /// <summary>
    /// Selects all books.
    /// </summary>
    internal class RemoveAllBooksPredicate : IPredicate<Book>
    {
        /// <inheritdoc />
        public bool Choose(Book item) => true;
    }
}
