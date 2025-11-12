using AutoMapper;
using ECommerce.Domain.Entity.ProductModule;
using ECommerce.Shared.Dtos.ProductsDtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ECommerce.Services.MappingProfiles
{
    public class ProductPictureUrlResolver : IValueResolver<Product, productDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, productDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
            {
                return string.Empty;
            }

            if (source.PictureUrl.StartsWith("http") || source.PictureUrl.StartsWith("https"))
                return source.PictureUrl;

            var BaseUrl = _configuration.GetSection("URLs")["BaseUrl"];
            var PictureUrl = $" {BaseUrl}{source.PictureUrl}";



            return PictureUrl;
        }
    }
}
