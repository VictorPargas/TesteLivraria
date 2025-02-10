using Microsoft.EntityFrameworkCore;
using MyBookRental.Domain.Entities;

namespace MyBookRental.Infrastructure.DataAccess
{
    public class MyBookRentalDbContext : DbContext
    {
        public MyBookRentalDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookRental> BooksRental { get; set; }
        public DbSet<BooksRentalDetail> BooksRentalDetails { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyBookRentalDbContext).Assembly);
        }
    }
}
