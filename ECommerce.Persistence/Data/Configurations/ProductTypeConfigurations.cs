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
    public class ProductTypeConfigurations : IEntityTypeConfiguration<ProductType>
    {

        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.Property(pt => pt.Name).HasMaxLength(100);
        }
    }
}
