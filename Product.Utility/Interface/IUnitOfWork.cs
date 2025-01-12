﻿namespace Product.Utility.Interface
{
    public interface IUnitOfWork
    {
        ICategoryService CategoryService { get;}
        Task SaveAsync();
    }
}