using ECommerce.Domain.Entity.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Data.Configurations
{
    internal class OrdedItemConfigurations : IEntityTypeConfiguration<OrderItems>
    {
        public void Configure(EntityTypeBuilder<OrderItems> builder)
        {
            builder.Property(x => x.Price).HasColumnType("decimal(8,2)");
            builder.OwnsOne(x => x.Product, OE =>
            {
                OE.Property(x => x.ProductName).HasMaxLength(100);
                OE.Property(x => x.PictureUrl).HasMaxLength(200);

            });
        }
    }
}
