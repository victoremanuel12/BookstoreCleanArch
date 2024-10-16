using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAuthorRepository
    {
        Task<ICollection<Author>> GetAll();
        Task<ICollection<Author>> GetAllWithBooks();
        Task Create(Author author);
    }
}
