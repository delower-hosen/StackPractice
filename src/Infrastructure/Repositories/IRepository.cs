using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, bool tracking = false);

        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        Task<int> SaveChangesAsync();
    }
}