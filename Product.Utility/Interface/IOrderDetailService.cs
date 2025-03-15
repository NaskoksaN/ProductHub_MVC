using ProductHub.DataAccess.Entities;
using ProductHub.DataAccess.Repository.Interface;

namespace ProductHub.Utility.Interface
{
    public interface IOrderDetailService : ISqlRepository<OrderDetail>
    {
        void Update(OrderDetail obj);
    }
}
