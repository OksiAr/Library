using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class Reader
    {
        public Reader()
        {
            Bookissuances = new HashSet<Bookissuance>();
        }

        public int NumberLibraryCard { get; set; }
        public string Lastname { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public virtual ICollection<Bookissuance> Bookissuances { get; set; }
    }
}
