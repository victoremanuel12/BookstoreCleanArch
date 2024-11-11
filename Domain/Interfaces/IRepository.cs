using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entity);
        void Update(T entity);
        void Delete(T entity);



    }
}
