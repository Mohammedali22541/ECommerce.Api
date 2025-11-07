using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entity.ProductModule
{
    public class ProductType : BaseEntity<int>
    {
        public string Name { get; set; } = default!;
    }
}
