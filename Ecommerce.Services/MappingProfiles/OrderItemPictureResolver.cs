using AutoMapper;
using ECommerce.Domain.Entity.OrderModule;
using ECommerce.Shared.Dtos.OrderDtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.MappingProfiles
{
    public class OrderItemPictureResolver : IValueResolver<OrderItems, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItems source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
                return string.Empty;

            if (source.Product.PictureUrl.StartsWith("http") || source.Product.PictureUrl.StartsWith("https"))
                return source.Product.PictureUrl;

            var baseUrl = _configuration.GetSection("URLs")["BaseUrl"];
            if (string.IsNullOrEmpty(baseUrl))
                return string.Empty;

            return $"{baseUrl}{source.Product.PictureUrl}";
            
               
            

        }
    }
}
