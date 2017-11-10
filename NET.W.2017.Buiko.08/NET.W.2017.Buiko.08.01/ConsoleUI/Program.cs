using System;
using System.Linq;
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
                Console.Error.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        private static void FileInitTests(IBookService bookService)
        {
            var book1 = new Book("978-3-16-148410-0", "author1", "name1", "publishing house1", "09.11.2017 22:40:51", 1, 1);
            var book2 = new Book("978-3-16-148411-1", "author2", "name2", "publishing house2", "09.11.2017 22:40:52", 2, 2);
            var book3 = new Book("978-3-16-148412-2", "author3", "name3", "publishing house3", "09.11.2017 22:40:53", 3, 3);
            var book4 = new Book("978-3-16-148413-3", "author4", "name4", "publishing house4", "09.11.2017 22:40:54", 4, 4);
            var book5 = new Book("978-3-16-148414-4", "author5", "name5", "publishing house5", "09.11.2017 22:40:55", 5, 5);

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
