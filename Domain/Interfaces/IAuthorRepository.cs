using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<IEnumerable<Author>> GetAllWithBooks();
        Task<Author> GetByIdAsync(Author author);
        Task CreateAsync(Author author);
        void Update(Author author);
        void Disable(Author author);
    }
}
