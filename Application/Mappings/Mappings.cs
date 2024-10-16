using Application.Dtos.Author;
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

        }
    }
}
