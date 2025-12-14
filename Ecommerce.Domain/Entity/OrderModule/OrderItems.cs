using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entity.OrderModule
{
    public class OrderItems : BaseEntity<int>
    {
        public ProductItemOrder Product { get; set; } = default!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
