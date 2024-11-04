using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Author>> GetAllWithBooks()
        {
            return await Get().Include(c => c.Books).ToListAsync();
        }
    }
}
