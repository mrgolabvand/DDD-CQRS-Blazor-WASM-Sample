using System.Linq.Expressions;

namespace _0_Framework.Infrastructure
{
    public interface IRepository<TKey, T> where T : class
    {
        Task CreateAsync(T entity);
        Task SaveChangesAsync();
        Task<T> GetByIdAsync(TKey id);
        bool Exists(Expression<Func<T, bool>> expression);
    }
}
