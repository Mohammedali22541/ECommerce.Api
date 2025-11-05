using ECommerce.Domain.Entity.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(100);

            builder.Property(p=>p.Description).HasMaxLength(500);

            builder.Property(p=>p.PictureUrl).HasMaxLength(200);

            builder.Property(p => p.Price).HasPrecision(18,2);

            builder.HasOne(p => p.ProductBrand).WithMany().HasForeignKey(p => p.ProductBrandId);

            builder.HasOne(p=>p.ProductType).WithMany().HasForeignKey(p=>p.ProductTypeId);

        }
    }
}
