using Microsoft.EntityFrameworkCore;
using Product.DataAccess.Repository.Interface;
using System.Linq.Expressions;

namespace Product.DataAccess.Repository
{
    public class SqlRepository<T> : ISqlRepository<T> where T : class
    {
        private readonly ApplicationDbContext db;
        internal DbSet<T> dbSet;
        public SqlRepository(ApplicationDbContext _db)
        {
            db= _db;
            this.dbSet= db.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
           await dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IQueryable<T> query = dbSet;

            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);

            return await query.FirstOrDefaultAsync();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
               
    }
}
