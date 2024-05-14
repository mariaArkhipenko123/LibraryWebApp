using System;
using Library.Domain;
using MediatR;

namespace Library.Application.Library.Commands.UpdateLibrary
{
    public class UpdateLibraryCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public int Isbn { get; set; }
    }
}
