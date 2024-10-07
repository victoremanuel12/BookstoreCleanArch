using Application.Dtos.Author;
using Application.Interfaces;
using Application.ServiceInterface;
using Domain.Entities;

namespace Application.Services
{
    public class AuthorService : IAuthorService
    {
        public IAuthorRepository _authorRepository;
        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthors()
        {
            IEnumerable<Author> listAutor = await _authorRepository.GetAll();
            IEnumerable<AuthorDto> dto = [];
            return dto;

        }
    }
}
