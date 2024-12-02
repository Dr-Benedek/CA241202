using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA241202
{
    internal class Author
    {
        public string FirstName { get; }
        public string LastName { get; }
        public Guid Id { get; }

        public Author(string fullName)
        {
            var names = fullName.Split(' ');
            if (names.Length < 2 || names[0].Length < 3 || names[1].Length < 3 ||
                names[0].Length > 32 || names[1].Length > 32)
            {
                throw new ArgumentException("A keresztnév és vezeték név hossza 3 és 32 karakter között kell, hogy legyen.");
            }

            FirstName = names[0];
            LastName = names[1];
            Id = Guid.NewGuid();
        }
    }
}
