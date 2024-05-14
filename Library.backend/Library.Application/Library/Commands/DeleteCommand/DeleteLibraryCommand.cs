using System;
using MediatR;

namespace Library.Application.Library.Commands.DeleteCommand
{
    public class DeleteLibraryCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}
