using System;
using System.Linq;
using Logger;
using Logic.Domain;
using Logic.Service;
using Logic.Storage;
using Logic.Storage.Implementation;

namespace ConsoleUI
{
    internal class Program
    {
        private static void Main()
        {
            var logger = LoggerFactory.GetNLogger("ConsoleUI.Program");
            try
            {
                // Seam.
                IBookStorage bookRepository = new BinaryFileBookStorage(@"books.bin");
                IBookService bookService = new BookService(bookRepository);

                if (bookService.GetBooks().ToArray().Length != 0)
                {
                    BookServiceTests(bookService);
                }
                else
                {
                    FileInitTests(bookService);
                }

                bookService.Save();
            }
            catch (Exception e)
            {
                logger.Error(e, e.Message);
            }
            
            Console.ReadLine();
        }

        private static void FileInitTests(IBookService bookService)
        {
            var book1 = new Book("978-3-16-148411-1", "author1", "name1", "publishing house1", "2017", 1, 1m);
            var book2 = new Book("978-3-16-148411-1", "author2", "name2", "publishing house2", "2017", 2, 2m);
            var book3 = new Book("978-3-16-148412-2", "author3", "name3", "publishing house3", "2017", 3, 3m);
            var book4 = new Book("978-3-16-148413-3", "author4", "name4", "publishing house4", "2017", 4, 4m);
            var book5 = new Book("978-3-16-148414-4", "author5", "name5", "publishing house5", "2017", 5, 5m);

            //// bookService.RemoveBook((Book)null);
            bookService.AddBook(book4, book5, book1, book2, book3);

            bookService.Sort();

            PrintBook(bookService.GetBooks().ToArray());

            PrintBook(bookService.FindBook("978-3-16-148414-4"));

            bookService.Save();
        }

        private static void BookServiceTests(IBookService bookService)
        {
            PrintBook(bookService.GetBooks().ToArray());
            PrintBook(bookService.FindBook("978-3-16-148410-0"));
            PrintBook(bookService.FindBook(new FindByNamePredicate()).ToArray());

            bookService.RemoveBook("978-3-16-148413-3");

            bookService.Sort();

            PrintBook(bookService.FindBook(new FindByNamePredicate()).ToArray());

            bookService.Save();
        }

        private static void PrintBook(params Book[] books)
        {
            foreach (var book in books)
            {
                Console.WriteLine(book);
            }

            Console.WriteLine();
        }
    }
}
