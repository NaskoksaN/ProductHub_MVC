using Product.DataAccess.Entities;
using Product.DataAccess.Repository.Interface;

namespace Product.Utility.Interface
{
    public interface ICategoryService : ISqlRepository<Category>
    {
        void Update(Category obj);
    }
}
