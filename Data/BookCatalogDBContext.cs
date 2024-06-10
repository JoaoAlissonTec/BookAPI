using BookAPI.Data.Map;
using BookAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BookAPI.Data
{
    public class BookCatalogDBContext : IdentityDbContext
    {
        public BookCatalogDBContext(DbContextOptions<BookCatalogDBContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Author> Authors {  get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookMap());
            modelBuilder.ApplyConfiguration(new AuthorMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
