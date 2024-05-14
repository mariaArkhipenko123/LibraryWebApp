using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Application.Library.Commands.CreateLibrary;
using Library.Domain;
using System.ComponentModel.DataAnnotations;

namespace Library.Application.Models
{

    public class CreateLibraryDto : IMapWith<CreateLibraryCommand>
    {
        [Required]
        public int Isbn { get; set; }
        [Required]
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        [Required]
        public Guid UserId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateLibraryDto, CreateLibraryCommand>()
                .ForMember(bookCommand => bookCommand.Isbn,
                    opt => opt.MapFrom(dto => dto.Isbn))
                .ForMember(bookCommand => bookCommand.Title,
                    opt => opt.MapFrom(dto => dto.Title))
                .ForMember(bookCommand => bookCommand.Genre,
                    opt => opt.MapFrom(dto => dto.Genre))
                .ForMember(bookCommand => bookCommand.Description,
                    opt => opt.MapFrom(dto => dto.Description))
                .ForMember(bookCommand => bookCommand.AuthorId,
                    opt => opt.MapFrom(dto => dto.AuthorId))
                .ForMember(bookCommand => bookCommand.FirstName,
                    opt => opt.MapFrom(dto => dto.FirstName))
                .ForMember(bookCommand => bookCommand.LastName,
                    opt => opt.MapFrom(dto => dto.LastName))
                .ForMember(bookCommand => bookCommand.DateOfBirth,
                    opt => opt.MapFrom(dto => dto.DateOfBirth))
                .ForMember(bookCommand => bookCommand.Country,
                    opt => opt.MapFrom(dto => dto.Country))
                .ForMember(bookCommand => bookCommand.UserId,
                    opt => opt.MapFrom(dto => dto.UserId));
        }
    }
}
