using System.Linq.Expressions;

namespace ProductHub.DataAccess.Repository.Interface
{
    public interface ISqlRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);
        Task AddAsync(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
