using AutoMapper;
using Domain.Dtos.Author;
using Domain.Dtos.Book;
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
            CreateMap<Book, BookDtoResponse>().ReverseMap();

        }
    }
}
