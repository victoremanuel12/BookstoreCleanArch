using Domain.Abstraction.PaginationFilter;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAuthorRepository
    {
        Task<int> CountAllRecords();
        Task<IEnumerable<Author>> GetAllAsync(PaginationFilter filter);
        Task<IEnumerable<Author>> GetAllWithBooks(PaginationFilter filter);
        Task<Author> GetByIdAsync(Author author);
        Task CreateAsync(Author author);
        void Update(Author author);
        void Disable(Author author);
    }
}
