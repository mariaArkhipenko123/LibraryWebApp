using AutoMapper;
using System;
using Library.Application.Common.Mappings;
using Library.Application.Library.Commands.UpdateLibrary;
using Library.Domain;

namespace Library.Application.Models
{
    public class UpdateLibraryDto : IMapWith<UpdateLibraryCommand>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public int Isbn { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateLibraryDto, UpdateLibraryCommand>()
                .ForMember(bookCommand => bookCommand.Id,
                    opt => opt.MapFrom(bookDto => bookDto.Id))
                .ForMember(bookCommand => bookCommand.UserId,
                    opt => opt.MapFrom(bookDto => bookDto.UserId))
                .ForMember(bookCommand => bookCommand.Title,
                    opt => opt.MapFrom(bookDto => bookDto.Title))
                .ForMember(bookCommand => bookCommand.Description,
                    opt => opt.MapFrom(bookDto => bookDto.Description))
                .ForMember(bookCommand => bookCommand.AuthorId,
                    opt => opt.MapFrom(bookDto => bookDto.AuthorId))
                .ForMember(bookCommand => bookCommand.FirstName,
                    opt => opt.MapFrom(bookDto => bookDto.FirstName))
                .ForMember(bookCommand => bookCommand.LastName,
                    opt => opt.MapFrom(bookDto => bookDto.LastName))
                .ForMember(bookCommand => bookCommand.DateOfBirth,
                    opt => opt.MapFrom(bookDto => bookDto.DateOfBirth))
                .ForMember(bookCommand => bookCommand.Country,
                    opt => opt.MapFrom(bookDto => bookDto.Country))
                .ForMember(bookCommand => bookCommand.Isbn,
                    opt => opt.MapFrom(bookDto => bookDto.Isbn));
        }
    }
}
