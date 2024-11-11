using Domain.Interfaces;
using Infra.Data;
using Microsoft.Extensions.DependencyInjection;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _context;
    private readonly IServiceProvider _serviceProvider;
    private bool _disposed = false;

    public UnitOfWork(IServiceProvider serviceProvider, AppDbContext context)
    {
        _context = context;
        _serviceProvider = serviceProvider;
        _context = context;
    }

    public IAuthorRepository AuthorRepository => _serviceProvider.GetService<IAuthorRepository>();

    public IReviewRepository ReviewRepository => _serviceProvider.GetService<IReviewRepository>();
    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}