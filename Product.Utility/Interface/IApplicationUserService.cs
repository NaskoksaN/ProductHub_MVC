using ProductHub.DataAccess.Entities;
using ProductHub.DataAccess.Repository.Interface;

namespace ProductHub.Utility.Interface
{
    public interface IApplicationUserService : ISqlRepository<ApplicationUser>
    {
       
    }
}
