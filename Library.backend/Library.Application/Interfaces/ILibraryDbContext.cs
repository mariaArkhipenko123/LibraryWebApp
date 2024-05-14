using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Library.Domain;

namespace Library.Application.Interfaces
{
    public interface ILibraryDbContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Author> Authors { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
