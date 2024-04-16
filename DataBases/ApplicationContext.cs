using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataBases
{
    public class ApplicationContext : DbContext
    {

        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Books{ get; set; }
        public DbSet<BookIssuance>  BookIssuances { get; set; }
        public DbSet<Genre>  Genres { get; set; }
        //public DbSet<Reader>  Readers { get; set; }
        public DbSet<Role>  Roles { get; set; }
        public DbSet<User>  Users { get; set; }


        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;user=root;password=1111;database=libraryy;",
                new MySqlServerVersion(new Version(8, 0, 11))
            );
        }

    }
}
