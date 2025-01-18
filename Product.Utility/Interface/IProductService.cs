using ProductHub.DataAccess.Entities;
using ProductHub.DataAccess.Repository.Interface;

namespace ProductHub.Utility.Interface
{
    public interface IProductService : ISqlRepository<Product>
    {
        void Update(Product obj);
    }
}
