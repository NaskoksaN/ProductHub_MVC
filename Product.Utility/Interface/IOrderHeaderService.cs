using ProductHub.DataAccess.Entities;
using ProductHub.DataAccess.Repository.Interface;

namespace ProductHub.Utility.Interface
{
    public interface IOrderHeaderService : ISqlRepository<OrderHeader>
    {
        void Update(OrderHeader obj);
        Task UpdateStatus(int id, string OrderStatus, string? paymentStatus=null);
        Task UpdateStripePaymentId(int id, string sessionId, string paymentIntentId);
    }
}
