using ECommerce.Domain.Entity.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string basketId);
        Task<CustomerBasket?> CreateOrUpdateBasket(CustomerBasket basket , TimeSpan timeToLive = default);

        Task<bool> DeleteBasketAsync(string basketId);
    }
}
