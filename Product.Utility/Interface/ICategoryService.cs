using Product.DataAccess.Entities;
using Product.DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Utility.Interface
{
    public interface ICategoryService : ISqlRepository<Category>
    {
        void Update(Category obj);
        Task SaveAsync();
    }
}
