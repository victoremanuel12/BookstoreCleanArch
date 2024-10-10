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

        

        public async Task<ICollection<Author>> GetAllWithBooks()
        {
            return await  _context.Authors.Include(b => b.Books).ToListAsync();
                 
        }

        async  Task<ICollection<Author>> IAuthorRepository.GetAll()
        {
            return await _context.Authors.ToListAsync();
        }
    }
}
