using System.Linq.Expressions;

namespace _0_Framework.Infrastructure
{
    public interface IRepositoryCqrs<TKey, T, TModel> where T : class
    {
        Task CreateAsync(T entity);
        Task SaveChangesAsync();
        Task<T> GetByIdAsync(TKey id);
        bool Exists(Expression<Func<T, bool>> expression);
        Task<TModel> GetDetailsAsync(TKey key);
    }
}
