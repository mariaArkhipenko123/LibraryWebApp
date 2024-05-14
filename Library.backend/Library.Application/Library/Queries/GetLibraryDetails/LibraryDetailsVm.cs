using System;
using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain;

namespace Library.Application.Library.Queries.GetLibraryDetails
{
    public class LibraryDetailsVm : IMapWith<Book>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int Isbn { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public DateTime AuthorDateOfBirth { get; set; }
        public string AuthorCountry { get; set; }
        public DateTime TimeTaken { get; set; }
        public DateTime? TimeToReturn { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Book, LibraryDetailsVm>()
                .ForMember(bookVm => bookVm.Id,
                    opt => opt.MapFrom(book => book.Id))
                .ForMember(bookVm => bookVm.Isbn,
                    opt => opt.MapFrom(book => book.Isbn))
                .ForMember(bookVm => bookVm.Title,
                    opt => opt.MapFrom(book => book.Title))
                .ForMember(bookVm => bookVm.Description,
                    opt => opt.MapFrom(book => book.Description))
                .ForMember(bookVm => bookVm.Genre,
                    opt => opt.MapFrom(book => book.Genre))
                .ForMember(bookVm => bookVm.AuthorId,
                    opt => opt.MapFrom(book => book.AuthorId))
                .ForMember(bookVm => bookVm.AuthorFirstName,
                    opt => opt.MapFrom(book => book.Author.FirstName))
                .ForMember(bookVm => bookVm.AuthorLastName,
                    opt => opt.MapFrom(book => book.Author.LastName))
                .ForMember(bookVm => bookVm.AuthorDateOfBirth,
                    opt => opt.MapFrom(book => book.Author.DateOfBirth))
                .ForMember(bookVm => bookVm.AuthorCountry,
                    opt => opt.MapFrom(book => book.Author.Country))
                .ForMember(bookVm => bookVm.TimeTaken,
                    opt => opt.MapFrom(book => book.TimeTaken))
                .ForMember(bookVm => bookVm.TimeToReturn,
                    opt => opt.MapFrom(book => book.TimeToReturn));
        }
    }
}


