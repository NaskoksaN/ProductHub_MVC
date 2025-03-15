

namespace ProductHub.Utility.ViewModels.ShopingCart
{
    public class ShoppingCartVM
    {
        public IEnumerable<ProductHub.DataAccess.Entities.ShopingCart> ShopingCartList { get; set; }
        public double OrderTotal { get; set; }
    }
}
