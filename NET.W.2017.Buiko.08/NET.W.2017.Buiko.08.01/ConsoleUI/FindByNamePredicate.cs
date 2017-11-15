using Logic.Domain;

namespace ConsoleUI
{
    /// <inheritdoc />
    public class FindByNamePredicate : IPredicate<Book>
    {
        /// <inheritdoc />
        public bool Choose(Book book) => book.Name.Contains("name");
    }
}
