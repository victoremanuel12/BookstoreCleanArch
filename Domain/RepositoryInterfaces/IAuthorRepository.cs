using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<IEnumerable<Author>> GetAllWithBooks();
    }
}
