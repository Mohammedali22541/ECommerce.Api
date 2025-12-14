using ECommerce.Domain.Entity.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Specifications.OrderSpecifications
{
    public class OrderSpecification : BaseSpecification<Order , Guid>
    {
        public OrderSpecification(string email):base(x=>x.UserEmail == email)
        {
            AddInclude(x => x.DeliveryMethods);
            AddInclude(x => x.items);
            AddOrderByDesc(x => x.OrderDate);

                
        }

        public OrderSpecification(string email , Guid id):base(x=>x.UserEmail== email && x.Id == id)
        {
            AddInclude(x => x.DeliveryMethods);
            AddInclude(x => x.items);
        }
    }
}
