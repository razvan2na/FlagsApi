using System.Linq.Expressions;

namespace FlagsApi.Repositories
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> Get(Expression<Func<T, bool>>? expression = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> Save();
    }
}
