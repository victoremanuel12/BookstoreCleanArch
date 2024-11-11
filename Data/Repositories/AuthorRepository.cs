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
            return await _repository.Get().Where(a => a.Books.Any()).Include(b => b.Books).ToListAsync();
        }

        public async Task CreateAsync(Author author)
        {
            await _repository.AddAsync(author);
        }
        public async Task<Author> GetByIdAsync(Author author)
        {
            var authorEntitiy = await _repository.GetByIdAsync(e => e.Id == author.Id);
            return authorEntitiy;
        }

        public void Update(Author author)
        {
            _repository.Update(author);
        }

        public void Disable(Author author)
        {
            author.IsActive = author.IsActive;
            _repository.Update(author);
        }
    }
}
