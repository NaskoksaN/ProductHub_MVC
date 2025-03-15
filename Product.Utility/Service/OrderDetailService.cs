using ProductHub.DataAccess.Data;
using ProductHub.DataAccess.Entities;
using ProductHub.DataAccess.Repository;
using ProductHub.Utility.Interface;

namespace ProductHub.Utility.Service
{
    public class OrderDetailService : SqlRepository<OrderDetail>, IOrderDetailService
    {
        private readonly ApplicationDbContext db;
        public OrderDetailService(ApplicationDbContext _db) : base(_db)
        {
            db = _db;
        }
       
        public void Update(OrderDetail obj)
        {
            db.OrderDetails.Update(obj);
        }
    }
}
