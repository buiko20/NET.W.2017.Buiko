using Logic.Domain;

namespace ConsoleUI
{
    public class FindByNamePredicate : IPredicate<Book>
    {
        public bool Choose(Book book) => book.Name.Contains("name");
    }
}
