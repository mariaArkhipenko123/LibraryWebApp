using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Library.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Library.Application.Common.Exceptions;
using Library.Domain;
using Library.Persistence.Repository;

namespace Library.Application.Library.Queries.GetLibraryDetails
{
    public class GetLibraryDetailsQueryHandler
         : IRequestHandler<GetLibraryDetailsQuery, LibraryDetailsVm>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLibraryDetailsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LibraryDetailsVm> Handle(GetLibraryDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.BookRepository.GetByID(request.Id);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Book), request.Id);
            }

            return _mapper.Map<LibraryDetailsVm>(entity);
        }

    }
}
