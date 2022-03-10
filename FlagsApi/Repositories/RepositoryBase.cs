using FlagsApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FlagsApi.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        internal ApplicationDbContext Context { get; }

        internal RepositoryBase(ApplicationDbContext context)
        {
            Context = context;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>>? expression = null)
        {
            if (expression is null)
                return Context.Set<T>();

            return Context.Set<T>().Where(expression);
        }

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            Context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public async Task<int> Save()
        {
            return await Context.SaveChangesAsync();
        }
    }
}
