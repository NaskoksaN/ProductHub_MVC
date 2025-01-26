

namespace ProductHub.Utility.ViewModels.ShopingCart
{
    public class ShopingCartVM
    {
        public IEnumerable<ProductHub.DataAccess.Entities.ShopingCart> ShopingcartList { get; set; } = [];
        public double OrderTotal { get; set; }
    }
}
