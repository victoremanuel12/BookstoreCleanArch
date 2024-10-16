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
        public async Task Create (Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

        }

        public async  Task<ICollection<Author>>GetAll()
        {
            return await _context.Authors.ToListAsync();
        }
    

        public async Task<ICollection<Author>> GetAllWithBooks()
        {
            return await  _context.Authors.Include(b => b.Books).ToListAsync();
                 
        }

    }
}
