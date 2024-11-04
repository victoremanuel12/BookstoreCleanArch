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

        public async Task Create(Author author)
        {
            await _repository.AddAsync(author);
        }
        public async Task<Author> GetByIdAsync(Author author)
        {
            var authorEntitiy = await _repository.GetByIdAsync(e => e.Id == author.Id);
            return authorEntitiy;
        }

        public Task Update(Author author)
        {
            return _repository.UpdateAsync(author);
        }

        public async Task Disable(Author author)
        {
            author.IsActive = author.IsActive;
            await _repository.UpdateAsync(author);
        }
    }
}
