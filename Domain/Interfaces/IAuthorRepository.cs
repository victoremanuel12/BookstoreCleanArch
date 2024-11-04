using Domain.Entities;
using Domain.Interfaces;

namespace Application.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<IEnumerable<Author>> GetAllWithBooks();
    }
}
