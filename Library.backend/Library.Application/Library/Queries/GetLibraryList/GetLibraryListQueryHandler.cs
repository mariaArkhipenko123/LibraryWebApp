using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Library.Application.Interfaces;
using Library.Persistence.Repository;

namespace Library.Application.Library.Queries.GetLibraryList
{
    public class GetLibraryListQueryHandler
        : IRequestHandler<GetLibraryListQuery, LibraryListVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLibraryListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LibraryListVm> Handle(GetLibraryListQuery request, CancellationToken cancellationToken)
        {
            var bookQuery = await _unitOfWork.BookRepository.Get(
                filter: book => book.UserId == request.UserId,
                orderBy: null,
                includeProperties: "");

            var totalCount = bookQuery.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);

            bookQuery = bookQuery
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);

            var bookDtos = _mapper.Map<IList<LibraryLookupDto>>(bookQuery);

            return new LibraryListVm
            {
                Books = bookDtos,
                TotalPages = totalPages,
                CurrentPage = request.PageNumber,
                TotalCount = totalCount
            };
        }


    }

}