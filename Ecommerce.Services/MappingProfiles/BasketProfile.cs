using AutoMapper;
using ECommerce.Domain.Entity.BasketModule;
using ECommerce.Shared.Dtos.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.MappingProfiles
{
    internal class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketDto, CustomerBasket>().ReverseMap();

            CreateMap<BasketItemsDto, BasketItem>().ReverseMap();
        }
    }

}
