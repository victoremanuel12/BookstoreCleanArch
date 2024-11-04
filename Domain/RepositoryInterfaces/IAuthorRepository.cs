using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<IEnumerable<Author>> GetAllWithBooks();
        Task Create(Author author);
        Task Update(Author author);
        Task Disable(Author author);
    }
}
