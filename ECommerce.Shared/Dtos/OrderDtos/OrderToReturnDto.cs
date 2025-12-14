using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Dtos.OrderDtos
{
    public class OrderToReturnDto
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }
        public AddressDto Address { get; set; }
        public string DeliveryMethod { get; set; }
        public string OrderStatus { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
