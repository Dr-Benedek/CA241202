using System;
using System.Collections.Generic;
using System.Linq;
using CA241202;
class Program
{
    public static Random rnd = new Random();
    static void Main(string[] args)
    {
        var bookList = CreateRandomBooks(15);
        SimulatePurchases(bookList);
    }

    private static List<Book> CreateRandomBooks(int count)
    {
        var titles = new List<string>()
            {
                "Elveszett idő nyomában", "Háború és béke", "A kis herceg",
                "1984", "A csodálatos Pókember", "Harry Potter és a bölcsek köve",
                "A Gyűrűk Ura", "A macska, aki megmentett egy könyvesboltot",
                "Pride and Prejudice", "Moby Dick", "To Kill a Mockingbird",
                "A szürke ötven árnyalata", "Bűn és bűnhődés", "Az alkimista", "Brave New World"
            };

        var authors = new List<string>()
            {
                "Gabriel García Márquez", "George Orwell", "Scott Fitzgerald",
                "Jane Austen", "J.K. Rowling", "Ernest Hemingway",
                "Leo Tolstoy", "Margaret Atwood", "Harper Lee"
            };

        var nyelvek = new List<string> { "magyar", "angol", "német" };

        var books = new List<Book>();
        for (int i = 0; i < count; i++)
        {
            var title = titles[rnd.Next(titles.Count)];
            var authorCount = rnd.Next(1, 4);
            var selectedAuthors = authors.OrderBy(a => rnd.Next()).Take(authorCount).Select(a => a).ToList();
            var nyelv = rnd.NextDouble() < 0.8 ? "magyar" : "angol";
            var KiadasEve = rnd.Next(2007, DateTime.Now.Year + 1);
            var stock = rnd.Next(0, 100) < 30 ? 0 : rnd.Next(5, 11);
            var price = rnd.Next(10, 101) * 100;

            books.Add(new Book(EgyediISBN(), selectedAuthors.Select(a => new Author(a)).ToList(), title, KiadasEve, nyelv, stock, price));
        }

        return books;
    }

    private static long EgyediISBN()
    {
        long LongRandom(long min, long max, Random rand)
        {
            long result = rand.Next((Int32)(min >> 32), (Int32)(max >> 32));
            result = (result << 32);
            result = result | (long)rand.Next((Int32)min, (Int32)max);
            return result;
        }
        long isbn;
        HashSet<long> uniqueisbn = new HashSet<long>();
        do
        {
            isbn = LongRandom(1000000000, 9999999999, new Random());
        } while (!uniqueisbn.Add(isbn));

        return isbn;
    }

    private static void SimulatePurchases(List<Book> books)
    {
        decimal osszBevetel = 0;
        int noRaktar = 0;
        int raktarkeszlet = books.Sum(b => b.Stock);
        int mostaniraktarK = raktarkeszlet;

        for (int i = 0; i < 100; i++)
        {
            var rndkonyv = books[rnd.Next(books.Count)];

            if (rndkonyv.Stock > 0)
            {
                osszBevetel += rndkonyv.Price;
                rndkonyv.Stock--;
            }
            else
            {
                if (rnd.NextDouble() < 0.5)
                {
                    rndkonyv.Stock += rnd.Next(1, 11);
                }
                else
                {
                    books.Remove(rndkonyv);
                    noRaktar++;
                }
            }
        }

        Console.WriteLine($"Összesítve eladott könyvek bruttó bevétele: {osszBevetel} Ft");
        Console.WriteLine($"Nagykerben elfogyott könyvek: {noRaktar}");
        Console.WriteLine($"Kezdeti készlet: {raktarkeszlet}, Jelenlegi készlet: {books.Sum(b => b.Stock)}");
        Console.WriteLine($"Készlet változás: {books.Sum(b => b.Stock) - raktarkeszlet}");
    }
}
