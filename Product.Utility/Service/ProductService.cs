using ProductHub.DataAccess;
using ProductHub.DataAccess.Entities;
using ProductHub.DataAccess.Repository;
using ProductHub.Utility.Interface;

namespace ProductHub.Utility.Service
{
    public class ProductService : SqlRepository<Product>, IProductService
    {
        private readonly ApplicationDbContext db;
        public ProductService(ApplicationDbContext _db) : base(_db)
        {
            db = _db;
        }

        public void Update(Product obj)
        {
            db.Products.Update(obj);
        }
    }
}
