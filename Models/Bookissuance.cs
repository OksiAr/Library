using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class Bookissuance
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int ReaderNumberLibraryCard { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime? DateOfReturn { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual Reader ReaderNumberLibraryCardNavigation { get; set; } = null!;
    }
}
