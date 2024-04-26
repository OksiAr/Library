using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Library.Models
{
    public partial class libraryyContext : DbContext
    {
        public libraryyContext()
        {
        }

        public libraryyContext(DbContextOptions<libraryyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Bookissuance> Bookissuances { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Reader> Readers { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("Server=localhost;Database=libraryy; username=root;password=1111");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("author");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(25)
                    .HasColumnName("firstname");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(25)
                    .HasColumnName("lastname");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(25)
                    .HasColumnName("patronymic");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("books");

                entity.HasIndex(e => e.AuthorId, "author_books_idx");

                entity.HasIndex(e => e.GenreId, "genre_books_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AuthorId).HasColumnName("authorId");

                entity.Property(e => e.CountCopies).HasColumnName("countCopies");

                entity.Property(e => e.GenreId).HasColumnName("genreId");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.Property(e => e.PublihingHouse)
                    .HasMaxLength(45)
                    .HasColumnName("publihingHouse");

                entity.Property(e => e.Year).HasColumnName("year");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("author_books");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("genre_books");
            });

            modelBuilder.Entity<Bookissuance>(entity =>
            {
                entity.ToTable("bookissuance");

                entity.HasIndex(e => e.BookId, "bookIssuance_book_idx");

                entity.HasIndex(e => e.ReaderNumberLibraryCard, "bookIssuance_book_idx1");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BookId).HasColumnName("bookId");

                entity.Property(e => e.DateOfIssue)
                    .HasColumnType("datetime")
                    .HasColumnName("dateOfIssue");

                entity.Property(e => e.DateOfReturn)
                    .HasColumnType("datetime")
                    .HasColumnName("dateOfReturn");

                entity.Property(e => e.ReaderNumberLibraryCard).HasColumnName("readerNumberLibraryCard");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Bookissuances)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bookIssuance_book");

                entity.HasOne(d => d.Reader)
                    .WithMany(p => p.Bookissuances)
                    .HasForeignKey(d => d.ReaderNumberLibraryCard)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bookIssuance_reader");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genres");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Reader>(entity =>
            {
                entity.HasKey(e => e.NumberLibraryCard)
                    .HasName("PRIMARY");

                entity.ToTable("readers");

                entity.Property(e => e.NumberLibraryCard).HasColumnName("numberLibraryCard");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(25)
                    .HasColumnName("firstname");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(25)
                    .HasColumnName("lastname");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(25)
                    .HasColumnName("patronymic");

                entity.Property(e => e.Phone)
                    .HasMaxLength(11)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.ReaderNumberCard, "users_reader_idx");

                entity.HasIndex(e => e.RoleId, "users_roles_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Login)
                    .HasMaxLength(15)
                    .HasColumnName("login");

                entity.Property(e => e.Password)
                    .HasMaxLength(15)
                    .HasColumnName("password");

                entity.Property(e => e.ReaderNumberCard).HasColumnName("readerNumberCard");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.HasOne(d => d.ReaderNumberCardNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ReaderNumberCard)
                    .HasConstraintName("users_reader");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("users_roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
