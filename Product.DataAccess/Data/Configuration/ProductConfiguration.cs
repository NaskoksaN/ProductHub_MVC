using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductHub.DataAccess.Entities;
using ProductHub.Models.Enums;

namespace ProductHub.DataAccess.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product
                {
                    Id = 1,
                    Name = "Soap1",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.",
                    Price=1.24,
                    Amount=120,
                    MeasurementUnit=MeasurementUnit.Grams,
                    ImgUrl="",
                    CategoryId=1
                },
                 new Product
                 {
                     Id = 2,
                     Name = "Soap2",
                     Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.",
                     Price = 1.40,
                     Amount = 150,
                     MeasurementUnit = MeasurementUnit.Grams,
                     ImgUrl = "",
                     CategoryId = 1
                 },
                  new Product
                  {
                      Id = 3,
                      Name = "Soap3",
                      Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.",
                      Price = 0.89,
                      Amount = 90,
                      MeasurementUnit = MeasurementUnit.Grams,
                      ImgUrl = "",
                      CategoryId = 1
                  },
                  new Product
                  {
                      Id = 4,
                      Name = "Soap4",
                      Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.",
                      Price = 1.30,
                      Amount = 140,
                      MeasurementUnit = MeasurementUnit.Grams,
                      ImgUrl = "",
                      CategoryId = 1
                  }, 
                  new Product
                  {
                      Id = 5,
                      Name = "PremiumSoap1",
                      Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.",
                      Price = 5.24,
                      Amount = 70,
                      MeasurementUnit = MeasurementUnit.Grams,
                      ImgUrl = "",
                      CategoryId = 3
                  },
                  new Product
                  {
                      Id = 6,
                      Name = "PremiumSoap2",
                      Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.",
                      Price = 4.24,
                      Amount = 60,
                      MeasurementUnit = MeasurementUnit.Grams,
                      ImgUrl = "",
                      CategoryId = 3
                  },
                  new Product
                  { 
                      Id=7,
                      Name = "PremiumSoap3",
                      Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.",
                      Price = 9.24,
                      Amount = 90,
                      MeasurementUnit = MeasurementUnit.Grams,
                      ImgUrl = "",
                      CategoryId = 3
                  },
                  new Product
                  {
                      Id=8,
                      Name = "PremiumSoap4",
                      Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.",
                      Price = 11.24,
                      Amount = 40,
                      MeasurementUnit = MeasurementUnit.Grams,
                      ImgUrl = "",
                      CategoryId = 3
                  },
                  new Product
                  {
                      Id=9,
                      Name = "ShowerGel1",
                      Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.",
                      Price = 15.24,
                      Amount = 550,
                      MeasurementUnit = MeasurementUnit.Milliliters,
                      ImgUrl = "",
                      CategoryId = 2
                  },
                  new Product
                  {
                      Id=10,
                      Name = "ShowerGel2",
                      Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.",
                      Price = 21.24,
                      Amount = 400,
                      MeasurementUnit = MeasurementUnit.Milliliters,
                      ImgUrl = "",
                      CategoryId = 2
                  },
                  new Product
                  {
                      Id=11,
                      Name = "Shampoo",
                      Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.",
                      Price = 112.24,
                      Amount = 400,
                      MeasurementUnit = MeasurementUnit.Milliliters,
                      ImgUrl = "",
                      CategoryId = 4
                  }

            );
        }
    }
}
