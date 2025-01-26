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

        public ICompanyService CompanyService { get; private set; }

        public IShopingCartService ShopingCartService { get; private set; }

        public IApplicationUserService ApplicationUserService { get; private set; }

        public UnitOfWork(ApplicationDbContext _db) 
        {
            db = _db;
            CategoryService = new CategoryService(_db);
            ProductService = new ProductService(_db);
            CompanyService = new CompanyService(_db);
            ShopingCartService = new ShopingCartService(_db);
            ApplicationUserService = new ApplicationUserService(_db);
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
