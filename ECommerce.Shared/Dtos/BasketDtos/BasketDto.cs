using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Dtos.BasketDtos
{
    public record BasketDto(string Id , ICollection<BasketItemsDto> Items);
   
}
