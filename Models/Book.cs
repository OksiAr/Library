using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class Book
    {
        public Book()
        {
            Bookissuances = new HashSet<Bookissuance>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public string PublihingHouse { get; set; } = null!;
        public int Year { get; set; }
        public int CountCopies { get; set; }

        public virtual Author Author { get; set; } = null!;
        public virtual Genre Genre { get; set; } = null!;
        public virtual ICollection<Bookissuance> Bookissuances { get; set; }
    }
}
