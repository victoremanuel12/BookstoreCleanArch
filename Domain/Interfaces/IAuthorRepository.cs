using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAll();
        Task<IEnumerable<Author>> GetAllWithBooks();
        Task Create(Author author);
    }
}
