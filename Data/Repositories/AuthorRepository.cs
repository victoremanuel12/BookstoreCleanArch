using Domain.Abstraction.PaginationFilter;
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
        public async Task<IEnumerable<Author>> GetAllAsync(PaginationFilter filter)
        {

            var pagedData = await _repository.Get()
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .OrderBy(a => a.Id)
                .ToListAsync();

            return pagedData;

        }
        public async Task<IEnumerable<Author>> GetAllWithBooks(PaginationFilter filter)
        {
            return await _repository.Get().Where(a => a.Books.Any()).Include(b => b.Books).ToListAsync();
        }
        public async Task<int> CountAllRecords()
        {
            return await _repository.Get().CountAsync();
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
