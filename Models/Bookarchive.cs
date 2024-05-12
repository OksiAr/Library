using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Bookarchive
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public int AuthorId { get; set; }
        public string AuthorFullName { get; set; }
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public string PublihingHouse { get; set; }
        public int Year { get; set; }
        public int CountCopies { get; set; }
    }
}
