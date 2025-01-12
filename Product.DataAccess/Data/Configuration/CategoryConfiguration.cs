using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Models.DataModels;

namespace Product.DataAccess.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category { Id = 1, Name = "Soap", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Shower Gel", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Premium Soap", DisplayOrder = 3 },
                new Category { Id = 4, Name = "Shampoo", DisplayOrder = 4 }
                );
        }
    }
}
