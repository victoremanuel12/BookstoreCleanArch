using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<IEnumerable<Author>> GetAllWithBooks();
        Task<Author> GetByIdAsync(Author author);
        Task CreateAsync(Author author);
        Task UpdateAsync(Author author);
        Task DisableAsync(Author author);
    }
}
