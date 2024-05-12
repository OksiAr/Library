using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public partial class Author
    {
        public string FullName { get => $"{Lastname} {Firstname} {Patronymic}"; }
    }
}
