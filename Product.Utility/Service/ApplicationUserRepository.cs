using ProductHub.DataAccess.Data;
using ProductHub.DataAccess.Entities;
using ProductHub.DataAccess.Repository;
using ProductHub.Utility.Interface;

namespace ProductHub.Utility.Service
{
    public class ApplicationUserRepository : SqlRepository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext db;
        public ApplicationUserRepository(ApplicationDbContext _db) : base(_db)
        {
            db = _db;
        }
        public void Update(ApplicationUser applicationUser)
        {
            db.ApplicationUsers.Update(applicationUser);
        }
    }
}
