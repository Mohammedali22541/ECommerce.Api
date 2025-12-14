using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entity.OrderModule
{
    public enum OrderStatus
    {
        Pending,
        PaymentRecived,
        PaymentFailed
    }
}
