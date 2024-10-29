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
            //Explicação da Projeção no Entity Framework(EF)
            //            Por que Usar Projeção?
            //Redução de dados: Apenas os campos necessários são carregados.
            //Desempenho: Menos dados são transferidos do banco de dados.
            //Consulta SQL eficiente: O EF gera uma consulta SQL enxuta, incluindo apenas as colunas solicitadas.
            //public async Task<IEnumerable<AuthorWithBooksDTO>> GetAllWithBooks()
            //{
            //    return await _context.Authors
            //        .Select(a => new AuthorWithBooksDTO
            //        {
            //            AuthorId = a.Id,
            //            AuthorName = a.Name,
            //            Books = a.Books.Select(b => new BookDTO
            //            {
            //                BookId = b.Id,
            //                BookName = b.Name
            //            }).ToList()
            //        })
            //        .AsNoTracking()
            //        .ToListAsync();
            //}
            return await _context.Authors.Include(b => b.Books).AsNoTracking().ToListAsync();


        }

    }
}
