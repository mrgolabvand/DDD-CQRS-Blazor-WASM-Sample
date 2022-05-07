using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace _0_Framework.Infrastructure
{
    public class RepositoryBaseCqrs<TKey, T, TModel> : IRepositoryCqrs<TKey, T, TModel> where T : class
    {
        private readonly DbContext dbContext;

        public RepositoryBaseCqrs(DbContext dbContext)
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

        public async Task<TModel> GetDetailsAsync(TKey key)
        {
            var config = new AutoMapper.MapperConfiguration(v =>
            {
                v.CreateMap<T, TModel>();
            });
            var mapper = config.CreateMapper();

            var DbModel = await dbContext.FindAsync<T>(key);

            var viewModel = mapper.Map<TModel>(DbModel);
            
            return viewModel;
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

    }
}
