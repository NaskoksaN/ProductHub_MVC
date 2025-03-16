using Microsoft.EntityFrameworkCore;
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

        public async Task UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var orderFromDb = await db.OrderHeaders.FirstOrDefaultAsync(x => x.Id == id);
            if ((orderFromDb != null))
            {
                orderFromDb.OrderStatus = orderStatus;
                if (!string.IsNullOrEmpty(paymentStatus))
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
        }

        public async Task UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
        {
            var orderFromDb = await db.OrderHeaders.FirstOrDefaultAsync(x => x.Id == id);
            if ((!string.IsNullOrEmpty(sessionId)))
            {
                orderFromDb.SeesionId = sessionId;

                if ((!string.IsNullOrEmpty(paymentIntentId)))
                {
                    orderFromDb.PaymentIntentId = paymentIntentId;
                    orderFromDb.PaymentDate = DateTime.Now;
                }
            }
        }
    }
}
