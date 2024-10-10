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

        public async Task<ICollection<AuthorDto>> Authors()
        {
            ICollection<Author> listAutor = await _authorRepository.GetAll();
            ICollection<AuthorDto> listAuthorDto = _mapper.Map<ICollection<AuthorDto>>(listAutor);
            return listAuthorDto;
        }

        public async Task<ICollection<AuthorDto>> AuthorsWithBooks()
        {
            ICollection<Author> authorWithBooks = await _authorRepository.GetAllWithBooks();
            ICollection<AuthorDto> authorWithBookDto = _mapper.Map<ICollection<AuthorDto>>(authorWithBooks);
            return authorWithBookDto;
        }
    }
}
