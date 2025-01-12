using Product.DataAccess;
using Product.DataAccess.Entities;
using Product.DataAccess.Repository;
using Product.Utility.Interface;

namespace Product.Utility.Service
{
    public class CatgeroyService :SqlRepository<Category>, ICategoryService
    {
        private readonly ApplicationDbContext db;
        public CatgeroyService(ApplicationDbContext _db) : base(_db)
        {
            db = _db;
        }
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Update(Category obj)
        {
            db.Categories.Update(obj);
        }
    }
}
