using ProductHub.DataAccess.Entities;

namespace ProductHub.Utility.ViewModels.Order
{
    public class OrderVM
    {
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<OrderDetail> OrderDetail { get; set; }
    }
}
