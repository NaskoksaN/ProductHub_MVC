namespace ProductHub.Utility.Interface
{
    public interface IUnitOfWork
    {
        ICategoryService CategoryService { get;}
        IProductService ProductService { get;}

        ICompanyService CompanyService { get; }
        Task SaveAsync();
    }
}
