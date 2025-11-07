using AutoMapper;
using ECommerce.Domain.Entity.ProductModule;
using ECommerce.Shared.Dtos.ProductsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductBrand, BrandDto>();

            CreateMap<ProductType , TypeDto>();

            CreateMap<Product, productDto>()
                .ForMember(dest=>dest.ProductBrand , opt=>opt
                .MapFrom(src=>src.ProductBrand.Name))
                .ForMember(dest=>dest.ProductType  , opt=>opt
                .MapFrom(src=>src.ProductType.Name));
        }
    }
}
