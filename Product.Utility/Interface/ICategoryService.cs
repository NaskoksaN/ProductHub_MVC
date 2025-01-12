using ProductHub.DataAccess.Entities;
using ProductHub.DataAccess.Repository.Interface;

namespace ProductHub.Utility.Interface
{
    public interface ICategoryService : ISqlRepository<Category>
    {
        void Update(Category obj);
    }
}
