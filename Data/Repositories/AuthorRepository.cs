using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        public AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Create(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            return await _context.Authors.AsNoTracking().ToListAsync();
        }


        public async Task<IEnumerable<Author>> GetAllWithBooks()
        {
            return await _context.Authors.Include(b => b.Books).AsNoTracking().ToListAsync();
        }

    }
}
