using ProductHub.DataAccess.Entities;
using ProductHub.DataAccess.Repository.Interface;

namespace ProductHub.Utility.Interface
{
    public interface IOrderHeaderService : ISqlRepository<OrderHeader>
    {
        void Update(OrderHeader obj);
    }
}
