using FlagsApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FlagsApi.Repositories
{
    public class RepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity> 
        where TEntity : class
        where TContext : DbContext
    {
        internal TContext Context { get; }

        internal RepositoryBase(TContext context)
        {
            Context = context;
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? expression = null)
        {
            if (expression is null)
                return Context.Set<TEntity>();

            return Context.Set<TEntity>().Where(expression);
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public async Task<int> Save()
        {
            return await Context.SaveChangesAsync();
        }
    }
}
