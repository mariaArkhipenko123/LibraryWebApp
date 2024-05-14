using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Library.Application.Interfaces;
using Library.Domain;
using static System.Reflection.Metadata.BlobBuilder;
using Library.Persistence.Repository;

namespace Library.Application.Library.Commands.CreateLibrary
{
    public class CreateLibraryCommandHandler
        : IRequestHandler<CreateLibraryCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateLibraryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateLibraryCommand request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.AuthorRepository.GetByID(request.AuthorId);
            if (author == null)
            {
                author = new Author
                {
                    Id = request.AuthorId,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    DateOfBirth = request.DateOfBirth,
                    Country = request.Country
                };

                await _unitOfWork.AuthorRepository.Insert(author);
                await _unitOfWork.SaveAsync(cancellationToken);
            }

            var book = new Book
            {
                UserId = request.UserId,
                Isbn = request.Isbn,
                Title = request.Title,
                Genre = request.Genre,
                Description = request.Description,
                AuthorId = author.Id,
                Id = Guid.NewGuid(),
                TimeTaken = DateTime.Now,
                TimeToReturn = null
            };

            await _unitOfWork.BookRepository.Insert(book);
            await _unitOfWork.SaveAsync(cancellationToken);

            return book.Id;
        }

    }
}
