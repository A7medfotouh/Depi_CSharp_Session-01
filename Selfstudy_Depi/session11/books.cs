using System;
using System.Collections.Generic;

namespace assignment11
{
    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string[] Authors { get; set; }
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }

        public Book(string _ISBN, string _Title,
                    string[] _Authors, DateTime _PublicationDate,
                    decimal _Price)
        {
            ISBN = _ISBN;
            Title = _Title;
            Authors = _Authors ?? Array.Empty<string>();   // عشان مفيش null
            PublicationDate = _PublicationDate;
            Price = _Price;
        }

        public override string ToString()
        {
            return $"ISBN: {ISBN}, Title: {Title}, Authors: {string.Join(", ", Authors)}, " +
                   $"Publication Date: {PublicationDate:yyyy-MM-dd}, Price: {Price:F2}";
        }
    }

    public class BookFunctions
    {
        public static string GetTitle(Book B)
        {
            return B.Title;
        }

        public static string GetAuthors(Book B)
        {
            return string.Join(", ", B.Authors);
        }

        public static string GetPrice(Book B)
        {
            return B.Price.ToString("F2");
        }
    }

    // (a) User-defined delegate بنفس الـ signature بتاع دوال BookFunctions
    public delegate string BookFunction(Book B);

    public class LibraryEngine
    {
        // (a) بيقبل user-defined delegate
        public static void ProcessBooks(List<Book> bList, BookFunction fPtr)
        {
            foreach (Book B in bList)
            {
                Console.WriteLine(fPtr(B));
            }
        }

    
        public static void ProcessBooksBuiltIn(List<Book> bList, Func<Book, string> fPtr)
        {
            foreach (Book B in bList)
            {
                Console.WriteLine(fPtr(B));
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Book> books = new List<Book>
            {
                new Book("111-AAA", "Clean Code", new[] { "Robert Martin" },
                         new DateTime(2008, 8, 1), 450.5m),
                new Book("222-BBB", "C# in Depth", new[] { "Jon Skeet", "Another Author" },
                         new DateTime(2019, 3, 15), 620m)
            };

            Console.WriteLine("(a) User-defined delegate:");
            LibraryEngine.ProcessBooks(books, BookFunctions.GetTitle);
            LibraryEngine.ProcessBooks(books, new BookFunction(BookFunctions.GetAuthors));

            Console.WriteLine("\n(b) Built-in delegate (Func<Book, string>):");
            LibraryEngine.ProcessBooksBuiltIn(books, BookFunctions.GetPrice);

            Console.WriteLine("\n(c) Anonymous method (GetISBN):");
            LibraryEngine.ProcessBooksBuiltIn(books, delegate (Book B)
            {
                return B.ISBN;
            });

            Console.WriteLine("\n(d) Lambda expression (GetPublicationDate):");
            LibraryEngine.ProcessBooksBuiltIn(books, B => B.PublicationDate.ToString("yyyy-MM-dd"));
        }
    }
}