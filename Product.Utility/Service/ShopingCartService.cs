using ProductHub.DataAccess.Data;
using ProductHub.DataAccess.Entities;
using ProductHub.DataAccess.Repository;
using ProductHub.Utility.Interface;

namespace ProductHub.Utility.Service
{
    public class ShopingCartService : SqlRepository<ShopingCart>, IShopingCartService
    {
        private readonly ApplicationDbContext db;
        public ShopingCartService(ApplicationDbContext _db) : base(_db)
        {
            db = _db;
        }
       
        public void Update(ShopingCart obj)
        {
            db.ShopingCarts.Update(obj);
        }
    }
}
