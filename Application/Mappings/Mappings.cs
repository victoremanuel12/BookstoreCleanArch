using Application.CQRS.Author.Command.CreateAuthorCommand;
using Application.Dtos.Author;
using Application.Dtos.Book;
using Application.Dtos.User;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Author, CreateAuthorCommand>()
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();
            CreateMap<Author, AuthorDtoResponse>().ReverseMap();
            CreateMap<Author, AuthorDtoRequest>().ReverseMap();
            CreateMap<Author, AuthorDtoWithBooksResponse>().ReverseMap();
            CreateMap<Book, BookDtoResponse>().ReverseMap();


        }
    }
}
