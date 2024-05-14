using System;
using MediatR;

namespace Library.Application.Library.Queries.GetLibraryList
{
    public class GetLibraryListQuery : IRequest<LibraryListVm>
    {
        public Guid UserId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
