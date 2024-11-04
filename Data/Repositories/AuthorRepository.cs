using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IRepository<Author> _repository;

        public AuthorRepository(IRepository<Author> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<Author>> GetAllWithBooks()
        {
            return await _repository.Get().Include(c => c.Books).ToListAsync();
        }
    }
}
