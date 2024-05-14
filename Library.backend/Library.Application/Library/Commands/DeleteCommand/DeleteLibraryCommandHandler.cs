using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Library.Application.Interfaces;
using Library.Application.Common.Exceptions;
using Library.Domain;
using Library.Application.Library.Commands.DeleteCommand;
using Library.Persistence.Repository;

namespace Library.Application.Library.Commands.DeleteCommand
{
    public class DeleteLibraryCommandHandler : IRequestHandler<DeleteLibraryCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLibraryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteLibraryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.BookRepository.GetByID(request.Id);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Book), request.Id);
            }

            _unitOfWork.BookRepository.Delete(entity);
            _unitOfWork.Save();

            return Unit.Value;
        }
    }
}