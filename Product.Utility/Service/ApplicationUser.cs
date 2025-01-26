using ProductHub.DataAccess.Data;
using ProductHub.DataAccess.Entities;
using ProductHub.DataAccess.Repository;
using ProductHub.Utility.Interface;

namespace ProductHub.Utility.Service
{
    public class ApplicationUserService : SqlRepository<ApplicationUser>, IApplicationUserService
    {
        private readonly ApplicationDbContext db;
        public ApplicationUserService(ApplicationDbContext _db) : base(_db)
        {
            db = _db;
        }
       
    }
}
