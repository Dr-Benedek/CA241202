using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA241202
{
    internal class Book
    {
        private static HashSet<long> uniqueisbn = new HashSet<long>();
        private long isbn;
        private List<Author> authors;
        private string title;
        private int kiadasEve;
        private string nyelv;
        private int stock;
        private int price;

        public long ISBN
        {
            get => isbn;
            private set
            {
                if (value < 1000000000 || value > 9999999999)
                    throw new Exception($"ISBN értéke {value} nem érvényes, 10 számjegyű legyen!");
                isbn = value;
            }
        }

        public List<Author> Authors
        {
            get => authors;
            private set
            {
                if (value == null || value.Count < 1 || value.Count > 3)
                    throw new Exception("A szerzők listájának 1 és 3 közötti szerzőt kell tartalmaznia.");
                authors = value;
            }
        }

        public string Title
        {
            get => title;
            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 3 || value.Length > 64)
                    throw new Exception($"A cím {value} hossza nem érvényes. Minimum 3, maximum 64 karakter hosszú kell legyen.");
                title = value;
            }
        }

        public int KiadasEve
        {
            get => kiadasEve;
            private set
            {
                if (value < 2007 || value > DateTime.Now.Year)
                    throw new Exception($"A kiadás éve {value} nem érvényes, 2007 és {DateTime.Now.Year} között kell legyen.");
                kiadasEve = value;
            }
        }

        public string Nyelv
        {
            get => nyelv;
            private set
            {
                if (value != "angol" && value != "német" && value != "magyar")
                    throw new Exception($"A nyelv {value} nem érvényes, csak az angol, német és magyar elfogadott.");
                nyelv = value;
            }
        }

        public int Stock
        {
            get => stock;
            set
            {
                if (value < 0)
                    throw new Exception($"A készlet értéke {value} nem érvényes, minimum 0 kell legyen.");
                stock = value;
            }
        }

        public int Price
        {
            get => price;
            private set
            {
                if (value < 1000 || value > 10000 || value % 100 != 0)
                    throw new Exception($"Az ár {value} nem érvényes, 1000 és 10000 közötti kerek 100-as szám kell legyen!!");
                price = value;
            }
        }

        public Book(long isbn, List<Author> authors, string title, int kiadasev,string nyelv, int stock, int price)
        {
            ISBN = isbn;
            Authors = authors;
            Title = title;
            KiadasEve = kiadasev;
            Nyelv = nyelv;
            Stock = stock;
            Price = price;
        }

        public Book(string title, string authorFullName)
        {
            ISBN = EgyediISBN();
            Authors = new List<Author> { new Author(authorFullName) };
            Title = title;
            KiadasEve = 2024;
            Nyelv = "magyar";
            Stock = 0;
            Price = 4500;
        }

        private long EgyediISBN()
        {
            long LongRandom(long min, long max, Random rand)
            {
                long result = rand.Next((Int32)(min >> 32), (Int32)(max >> 32));
                result = (result << 32);
                result = result | (long)rand.Next((Int32)min, (Int32)max);
                return result;
            }
            long isbn;
            do
            {
                isbn = LongRandom(1000000000, 9999999999, new Random());
            } while (!uniqueisbn.Add(isbn));
            return isbn;
        }

        public override string ToString()
        {
            var authorLabel = Authors.Count == 1 ? "szerző:" : "szerzők:";
            var stockInfo = Stock == 0 ? "beszerzés alatt" : $"{Stock} db";

            return $"{Title} ({ISBN})\n{authorLabel} {string.Join(", ", Authors.Select(a => $"{a.FirstName} {a.LastName}"))}\n" +
                    $"Kiadás éve: {KiadasEve}, Nyelv: {Nyelv}, Ár: {Price} Ft, Készlet: {stockInfo}";
        }
    }
}
