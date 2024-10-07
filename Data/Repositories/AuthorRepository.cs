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

       async  Task<IEnumerable<Author>> IAuthorRepository.GetAll()
        {
            return await _context.Authors.ToListAsync();
        }
    }
}
