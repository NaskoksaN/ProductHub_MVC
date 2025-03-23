using ProductHub.DataAccess.Entities;
using ProductHub.DataAccess.Repository.Interface;

namespace ProductHub.Utility.Interface
{
    public interface IApplicationUserRepository : ISqlRepository<ApplicationUser>
    {
        public void Update(ApplicationUser applicationUser);
    }
}
