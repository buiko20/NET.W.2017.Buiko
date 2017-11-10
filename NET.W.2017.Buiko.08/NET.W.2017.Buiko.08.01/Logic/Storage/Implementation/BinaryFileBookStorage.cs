using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Logic.Domain;

namespace Logic.Storage.Implementation
{
    public class BinaryFileBookStorage : IBookStorage
    {
        #region private fields

        private readonly string _dataFilePath;

        #endregion // !private fields.

        #region public 

        #region constructors

        /// <summary>
        /// Initializes an instance of storage with the passed parameter.
        /// </summary>
        /// <param name="dataFilePath">book isbn</param>
        public BinaryFileBookStorage(string dataFilePath)
        {
            if (string.IsNullOrWhiteSpace(dataFilePath))
            {
                throw new ArgumentException("Invalid file path", nameof(dataFilePath));
            }

            _dataFilePath = dataFilePath;
        }

        #endregion // !constructors.

        #region implementation of the interface

        /// <inheritdoc />
        public IEnumerable<Book> GetBooks()
        {
            var books = new List<Book>();
            using (var binaryReader = new BinaryReader(File.Open(_dataFilePath, FileMode.Open, FileAccess.Read, FileShare.Read), Encoding.UTF8, false))
            {
                while (binaryReader.PeekChar() > -1)
                {
                    var book = ReadBook(binaryReader);
                    books.Add(book);
                }
            }

            return books;
        }

        /// <inheritdoc />
        public void Save(IEnumerable<Book> books)
        {
            using (var binaryWriter = new BinaryWriter(File.Open(_dataFilePath, FileMode.Create, FileAccess.Write, FileShare.None), Encoding.UTF8, false))
            {
                foreach (var book in books)
                {
                    WriteBook(binaryWriter, book);
                }
            }
        }

        #endregion // !implementation of the interface.

        #endregion // !public.

        #region private

        private static Book ReadBook(BinaryReader binaryReader)
        {
            var isbn = binaryReader.ReadString();
            var author = binaryReader.ReadString();
            var name = binaryReader.ReadString();
            var publishingHouse = binaryReader.ReadString();
            var publicationYear = binaryReader.ReadString();
            var pageNumber = binaryReader.ReadInt32();
            var price = binaryReader.ReadInt32();

            return new Book(isbn, author, name, publishingHouse, publicationYear, pageNumber, price);
        }

        private static void WriteBook(BinaryWriter binaryWriter, Book book)
        {
            binaryWriter.Write(book.Isbn);
            binaryWriter.Write(book.Author);
            binaryWriter.Write(book.Name);
            binaryWriter.Write(book.PublishingHouse);
            binaryWriter.Write(book.PublicationYear);
            binaryWriter.Write(book.PageNumber);
            binaryWriter.Write(book.Price);
        }

        #endregion // !private.
    }
}
