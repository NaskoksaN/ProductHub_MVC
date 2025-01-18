namespace ProductHub.Utility.Interface
{
    public interface IUnitOfWork
    {
        ICategoryService CategoryService { get;}
        IProductService ProductService { get;}
        Task SaveAsync();
    }
}
