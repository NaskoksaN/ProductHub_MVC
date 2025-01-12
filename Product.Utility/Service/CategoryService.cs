using ProductHub.DataAccess;
using ProductHub.DataAccess.Entities;
using ProductHub.DataAccess.Repository;
using ProductHub.Utility.Interface;

namespace ProductHub.Utility.Service
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
