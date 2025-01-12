using Product.DataAccess;
using Product.Utility.Interface;

namespace Product.Utility.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext db;

        public ICategoryService CategoryService { get; private set; }

        public UnitOfWork(ApplicationDbContext _db) 
        {
            db = _db;
            CategoryService = new CategoryService(_db);
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
