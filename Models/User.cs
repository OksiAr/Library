using System;
using System.Collections.Generic;

namespace Library.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RoleId { get; set; }
        public int? ReaderNumberCard { get; set; }

        public virtual Reader? ReaderNumberCardNavigation { get; set; }
        public virtual Role Role { get; set; } = null!;
    }
}
