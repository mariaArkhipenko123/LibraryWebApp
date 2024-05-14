using System;
using MediatR;

namespace Library.Application.Library.Queries.GetLibraryDetails
{
    public class GetLibraryDetailsQuery : IRequest<LibraryDetailsVm>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}
