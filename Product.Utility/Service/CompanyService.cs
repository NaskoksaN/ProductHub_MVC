using ProductHub.DataAccess.Data;
using ProductHub.DataAccess.Entities;
using ProductHub.DataAccess.Repository;
using ProductHub.Utility.Interface;

namespace ProductHub.Utility.Service
{
    public class CompanyService : SqlRepository<Company>, ICompanyService
    {
        private readonly ApplicationDbContext db;
        public CompanyService(ApplicationDbContext _db) : base(_db)
        {
            db = _db;
        }
        public void Update(Company obj)
        {
            db.Companies.Update(obj);
        }
    }
}
