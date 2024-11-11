using Application.Dtos.Author;
using Application.Dtos.Book;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Author, AuthorDtoResponse>().ReverseMap();
            CreateMap<Author, AuthorDtoRequest>().ReverseMap();
            CreateMap<Author, AuthorWithBooksDtoRequest>().ReverseMap();
            CreateMap<Author, AuthorDisableDto>().ReverseMap();
            CreateMap<Book, BookDtoResponse>().ReverseMap();

        }
    }
}
