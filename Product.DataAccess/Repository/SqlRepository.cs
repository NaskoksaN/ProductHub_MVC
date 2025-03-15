using Microsoft.EntityFrameworkCore;
using ProductHub.DataAccess.Data;
using ProductHub.DataAccess.Repository.Interface;
using System.Linq.Expressions;

namespace ProductHub.DataAccess.Repository
{
    public class SqlRepository<T> : ISqlRepository<T> where T : class
    {
        private readonly ApplicationDbContext db;
        internal DbSet<T> dbSet;
        public SqlRepository(ApplicationDbContext _db)
        {
            db= db = _db ?? throw new ArgumentNullException(nameof(_db));
            this.dbSet= db.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
           await dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter, 
                                                            string? includeProperties=null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter,
                string? includeProperties = null,
                bool tracked = false)
        {
            IQueryable<T> query;
            query = tracked ? dbSet : dbSet.AsNoTracking();
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
          
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
