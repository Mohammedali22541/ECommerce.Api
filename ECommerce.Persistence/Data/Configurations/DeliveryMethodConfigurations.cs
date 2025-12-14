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
    internal class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethods>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethods> builder)
        {
            builder.Property(x => x.Price).HasColumnType("decimal(8,2)");
            builder.Property(x => x.ShortName).HasMaxLength(50);
            builder.Property(x => x.DeliveryTime).HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(100);
        }
    }
}
