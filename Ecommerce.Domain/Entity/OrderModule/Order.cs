using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entity.OrderModule
{
    public class Order : BaseEntity<Guid>
    {
        public string UserEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus status { get; set; } = OrderStatus.Pending;
        public OrderAddress Address { get; set; } = default!;
        public DeliveryMethods DeliveryMethods { get; set; } = default!;
        public int DeliveryMethodsId { get; set; }
        public ICollection<OrderItems> items { get; set; } = [];
        public decimal SubTotal { get; set; }
        public decimal GetTotal() => SubTotal + DeliveryMethods.Price;
    }
}
