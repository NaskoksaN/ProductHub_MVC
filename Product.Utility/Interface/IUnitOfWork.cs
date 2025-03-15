namespace ProductHub.Utility.Interface
{
    public interface IUnitOfWork
    {
        ICategoryService CategoryService { get;}
        IProductService ProductService { get;}
        IShopingCartService ShopingCartService { get;}
        ICompanyService CompanyService { get; }
        IApplicationUserRepository ApplicationUserRepository { get; }
        IOrderDetailService OrderDetailService { get; }
        IOrderHeaderService OrderHeaderService { get; }
        Task SaveAsync();
    }
}
