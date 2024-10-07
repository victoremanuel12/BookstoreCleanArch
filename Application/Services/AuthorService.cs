using Application.Dtos.Author;
using Application.Interfaces;
using Application.ServiceInterface;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class AuthorService : IAuthorService
    {
        public IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorDto>> Authors()
        {
            IEnumerable<Author> listAutor = await _authorRepository.GetAll();
            IEnumerable<AuthorDto> listAuthorDto = _mapper.Map<IEnumerable<AuthorDto>>(listAutor);
            return listAuthorDto;
        }
    }
}
