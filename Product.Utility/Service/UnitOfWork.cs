using ProductHub.DataAccess;
using ProductHub.DataAccess.Data;
using ProductHub.Utility.Interface;

namespace ProductHub.Utility.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext db;

        public ICategoryService CategoryService { get; private set; }

        public IProductService ProductService { get; private set; }

        public UnitOfWork(ApplicationDbContext _db) 
        {
            db = _db;
            CategoryService = new CategoryService(_db);
            ProductService = new ProductService(_db);
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
