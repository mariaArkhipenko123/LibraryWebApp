using Microsoft.EntityFrameworkCore;
using Library.Application.Interfaces;
using Library.Domain;
using Library.Persistense.EntityTypeConfigurations;
using static Library.Application.Interfaces.ILibraryDbContext;

namespace Library.Persistense
{
    public class LibraryDbContext : DbContext, ILibraryDbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LibraryConfiguration());
        }
    }
}
