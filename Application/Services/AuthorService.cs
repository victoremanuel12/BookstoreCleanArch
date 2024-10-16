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
        public async Task<IEnumerable<AuthorDtoResponse>> Authors()
        {
            IEnumerable<Author> listAutor = await _authorRepository.GetAll();
            IEnumerable<AuthorDtoResponse> listAuthorDto = _mapper.Map<IEnumerable<AuthorDtoResponse>>(listAutor);
            return listAuthorDto;
        }

        public async Task<IEnumerable<AuthorDtoResponse>> AuthorsWithBooks()
        {
            IEnumerable<Author> authorWithBooks = await _authorRepository.GetAllWithBooks();
            IEnumerable<AuthorDtoResponse> authorWithBookDto = _mapper.Map<IEnumerable<AuthorDtoResponse>>(authorWithBooks);
            return authorWithBookDto;
        }
    }
}
