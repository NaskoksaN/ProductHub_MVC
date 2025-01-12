using Product.DataAccess;
using Product.DataAccess.Entities;
using Product.DataAccess.Repository;
using Product.Utility.Interface;

namespace Product.Utility.Service
{
    public class CategoryService :SqlRepository<Category>, ICategoryService
    {
        private readonly ApplicationDbContext db;
        public CategoryService(ApplicationDbContext _db) : base(_db)
        {
            db = _db;
        }
       
        public void Update(Category obj)
        {
            db.Categories.Update(obj);
        }
    }
}
