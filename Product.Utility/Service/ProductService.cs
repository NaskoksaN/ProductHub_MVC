using ProductHub.DataAccess;
using ProductHub.DataAccess.Entities;
using ProductHub.DataAccess.Repository;
using ProductHub.Models.ViewModels.Product;
using ProductHub.Utility.Interface;
using System.Drawing.Printing;

namespace ProductHub.Utility.Service
{
    public class ProductService : SqlRepository<Product>, IProductService
    {
        private readonly ApplicationDbContext db;
        public ProductService(ApplicationDbContext _db) : base(_db)
        {
            db = _db;
        }

        public void Update(ProductFormModel obj)
        {
            Product? product = db.Products.FirstOrDefault(x => x.Id == obj.Id);
            if (product != null)
            {
                product.Name = obj.Name;
                product.Description = obj.Description;
                product.Price = obj.Price;
                product.Amount = obj.Amount;
                product.MeasurementUnit = obj.MeasurementUnit;
               
                product.CategoryId = obj.CategoryId;
                if (!string.IsNullOrEmpty(obj.ImgUrl))
                {
                    product.ImgUrl = obj.ImgUrl;
                }
                db.Products.Update(product);
            }
            
        }
    }
}
