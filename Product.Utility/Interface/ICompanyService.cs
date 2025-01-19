using ProductHub.DataAccess.Entities;
using ProductHub.DataAccess.Repository.Interface;

namespace ProductHub.Utility.Interface
{
    public interface ICompanyService : ISqlRepository<Company>
    {
        void Update(Company obj);
    }
}
