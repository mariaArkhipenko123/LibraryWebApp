using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Library.Application.Interfaces;
using Library.Application.Common.Exceptions;
using Library.Domain;
using Library.Persistence.Repository;

namespace Library.Application.Library.Commands.UpdateLibrary
{
    public class UpdateLibraryCommandHandler : IRequestHandler<UpdateLibraryCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLibraryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateLibraryCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.BookRepository.GetByID(request.Id);

            if (book == null || book.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Book), request.Id);
            }

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
            }
            else
            {
                author.FirstName = request.FirstName;
                author.LastName = request.LastName;
                author.DateOfBirth = request.DateOfBirth;
                author.Country = request.Country;

                _unitOfWork.AuthorRepository.Update(author);
            }

            book.Description = request.Description;
            book.Title = request.Title;
            book.Isbn = request.Isbn;
            book.AuthorId = author.Id;
            book.TimeTaken = DateTime.Now;

            _unitOfWork.BookRepository.Update(book);
            await _unitOfWork.SaveAsync(cancellationToken);

            return Unit.Value;
        }


    }
}
