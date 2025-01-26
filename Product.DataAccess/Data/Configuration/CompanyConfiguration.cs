using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductHub.DataAccess.Entities;

namespace ProductHub.DataAccess.Configuration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData(
                new Company
                {
                    Id = 1,
                    Name = "Nasko Bobchev Ltd",
                    VAT = "BG41023450",
                    City = "Varna",
                    PostalCode = "BG 9000",
                    StreetAddress = "jk Pobeda, str Todor radev Penev 7 - ap16",
                    PhoneNumber = "0012312334"
                },
                new Company
                {
                    Id=2,
                    Name = "Nasko  Ltd",
                    VAT = "BG41023123",
                    City = "Haskovo",
                    PostalCode = "BG 6300",
                    StreetAddress = "str Gurgulqt 2, ent.B , app58",
                    PhoneNumber = "0023312334"
                },
                new Company
                {
                    Id = 3,
                    Name = "Mega company Ltd",
                    VAT = "BG41034123",
                    City = "selo Kukovica",
                    PostalCode = "BG 1300",
                    StreetAddress = "str GPetko petkov 12",
                    PhoneNumber = "01223312334"
                });
        }
    }
}
