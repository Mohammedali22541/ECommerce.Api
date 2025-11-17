using ECommerce.Shared.Dtos.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Abstraction
{
    public interface IBasketServices
    {
        Task<BasketDto> CreateOrUpdateAsync ( BasketDto createOrUpdateBasket);

        Task<BasketDto> GetBasketAsync ( string BasketId);

        Task<bool> DeleteBasketAsync ( string BasketId);
    }
}
