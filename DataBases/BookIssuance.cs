using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataBases
{
    public class BookIssuance
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int readerNumberLibraryCard { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime DateOfReturn { get; set; }


    }
}
