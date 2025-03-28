﻿using ProductHub.DataAccess.Entities;
using ProductHub.DataAccess.Repository.Interface;
using ProductHub.Utility.ViewModels.Product;

namespace ProductHub.Utility.Interface
{
    public interface IProductService : ISqlRepository<Product>
    {
        void Update(ProductFormModel obj);
    }
}
