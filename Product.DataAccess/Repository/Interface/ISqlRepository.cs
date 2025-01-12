using System.Linq.Expressions;

namespace Product.DataAccess.Repository.Interface
{
    public interface ISqlRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task AddAsync(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
