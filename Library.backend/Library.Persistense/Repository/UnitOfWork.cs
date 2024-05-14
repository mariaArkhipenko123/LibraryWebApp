using System;
using Library.Domain;
using Library.Persistence.Repository;
using Library.Persistense.Repository;
using Library.Persistense;

namespace Library.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly LibraryDbContext _context;
        private IGenericRepository<Book> _bookRepository;
        private IGenericRepository<Author> _authorRepository;
        private bool _disposed;

        public UnitOfWork(LibraryDbContext context)
        {
            _context = context;
            _bookRepository = new GenericRepository<Book>(context);
            _authorRepository = new GenericRepository<Author>(context);
        }

        public IGenericRepository<Book> BookRepository => _bookRepository;
        public IGenericRepository<Author> AuthorRepository => _authorRepository;

        public void Save()
        {
            _context.SaveChanges();
        }
        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
