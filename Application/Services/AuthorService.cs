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
        public async Task NewAuthor(AuthorDtoRequest authorDtoRequest)
        {
            Author author = _mapper.Map<Author>(authorDtoRequest);
            await _authorRepository.Create(author);
        }
        public async Task<ICollection<AuthorDtoResponse>> Authors()
        {
            ICollection<Author> listAutor = await _authorRepository.GetAll();
            ICollection<AuthorDtoResponse> listAuthorDto = _mapper.Map<ICollection<AuthorDtoResponse>>(listAutor);
            return listAuthorDto;
        }

        public async Task<ICollection<AuthorDtoResponse>> AuthorsWithBooks()
        {
            ICollection<Author> authorWithBooks = await _authorRepository.GetAllWithBooks();
            ICollection<AuthorDtoResponse> authorWithBookDto = _mapper.Map<ICollection<AuthorDtoResponse>>(authorWithBooks);
            return authorWithBookDto;
        }
    }
}
