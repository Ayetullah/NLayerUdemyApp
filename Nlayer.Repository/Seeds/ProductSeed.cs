
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace Nlayer.Repository.Seeds
{
    public class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product
                {
                    Id = 1,
                    CategoryId = 1,
                    Price = 100,
                    Stock = 20,
                    CreatedDate = DateTime.Now,
                    Name = "Kalem1"
                }, new Product
                {
                    Id = 2,
                    CategoryId = 1,
                    Price = 200,
                    Stock = 40,
                    CreatedDate = DateTime.Now,
                    Name = "Kalem2"
                }, new Product
                {
                    Id = 3,
                    CategoryId = 2,
                    Price = 50,
                    Stock = 40,
                    CreatedDate = DateTime.Now,
                    Name = "Kitap2"
                });
        }
    }
}
