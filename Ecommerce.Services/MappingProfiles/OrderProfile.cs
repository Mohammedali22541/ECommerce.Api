using AutoMapper;
using ECommerce.Domain.Entity.OrderModule;
using ECommerce.Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDto, OrderAddress>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(x => x.DeliveryMethod, opt => opt.MapFrom(
                    src => src.DeliveryMethods.ShortName))
            .ForMember(x => x.OrderStatus, opt => opt.MapFrom(src =>
            src.status));
            CreateMap<OrderItems, OrderItemDto>()
                .ForMember(x => x.ProductName, opt => opt.MapFrom(
                    src => src.Product.ProductName))
                 .ForMember(x => x.ProductName, opt => opt.MapFrom<OrderItemPictureResolver>());

            CreateMap<DeliveryMethods, DeliveryMethodDto>();








        }


    }
}
