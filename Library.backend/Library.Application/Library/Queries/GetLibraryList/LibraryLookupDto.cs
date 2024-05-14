using AutoMapper;
using System;
using Library.Application.Common.Mappings;
using Library.Domain;

namespace Library.Application.Library.Queries.GetLibraryList
{
    public class LibraryLookupDto : IMapWith<Book>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public int Isbn { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Book, LibraryLookupDto>()
                .ForMember(bookDto => bookDto.Id,
                    opt => opt.MapFrom(book => book.Id))
                .ForMember(bookDto => bookDto.Title,
                    opt => opt.MapFrom(book => book.Title))
                .ForMember(bookDto => bookDto.Description,
                    opt => opt.MapFrom(book => book.Description))
                .ForMember(bookDto => bookDto.Genre,
                    opt => opt.MapFrom(book => book.Genre))
                .ForMember(bookDto => bookDto.AuthorId,
                    opt => opt.MapFrom(book => book.AuthorId))
                .ForMember(bookDto => bookDto.AuthorFirstName,
                    opt => opt.MapFrom(book => book.Author.FirstName))
                .ForMember(bookDto => bookDto.AuthorLastName,
                    opt => opt.MapFrom(book => book.Author.LastName))
                .ForMember(bookDto => bookDto.Isbn,
                    opt => opt.MapFrom(book => book.Isbn));
        }
    }
}

