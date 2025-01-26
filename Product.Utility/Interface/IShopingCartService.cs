using ProductHub.DataAccess.Entities;
using ProductHub.DataAccess.Repository.Interface;

namespace ProductHub.Utility.Interface
{
    public interface IShopingCartService : ISqlRepository<ShopingCart>
    {
        void Update(ShopingCart obj);
    }
}
