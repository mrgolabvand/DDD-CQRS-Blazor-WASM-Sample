using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace _0_Framework.Infrastructure
{
    public class RepositoryBase<TKey, T> : IRepository<TKey, T> where T : class
    {
        private readonly DbContext dbContext;

        public RepositoryBase(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(T entity)
        {
            await dbContext.AddAsync(entity);
            await SaveChangesAsync();
        }

        public bool Exists(Expression<Func<T, bool>> expression)
        {
            return dbContext.Set<T>().Any(expression);
        }

        public async Task<T> GetByIdAsync(TKey id)
        {
            return await dbContext.FindAsync<T>(id);
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

    }
}
