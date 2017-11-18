using System;

namespace Collection.NUnitTests
{
    public class Book : IComparable<Book>
    {
        public readonly string Title;

        public Book(string title)
        {
            Title = title;
        }

        public int CompareTo(Book other) =>
            ReferenceEquals(other, null) ? 1 : 
            string.CompareOrdinal(this.Title, other.Title);
    }
}
