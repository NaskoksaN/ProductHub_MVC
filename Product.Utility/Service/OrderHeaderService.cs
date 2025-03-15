using ProductHub.DataAccess.Data;
using ProductHub.DataAccess.Entities;
using ProductHub.DataAccess.Repository;
using ProductHub.Utility.Interface;

namespace ProductHub.Utility.Service
{
    public class OrderHeaderService : SqlRepository<OrderHeader>, IOrderHeaderService
    {
        private readonly ApplicationDbContext db;
        public OrderHeaderService(ApplicationDbContext _db) : base(_db)
        {
            db = _db;
        }
       
        public void Update(OrderHeader obj)
        {
            db.OrderHeaders.Update(obj);
        }
    }
}
