using Library.Domain;
using Library.Persistense.Repository;

namespace Library.Persistence.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Book> BookRepository { get; }
        IGenericRepository<Author> AuthorRepository { get; }
        Task SaveAsync(CancellationToken cancellationToken);

        void Save();
    }
}
