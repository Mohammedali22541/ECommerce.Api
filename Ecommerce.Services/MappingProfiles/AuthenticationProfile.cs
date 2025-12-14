using AutoMapper;
using ECommerce.Domain.Entity.IdentityModule;
using ECommerce.Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.MappingProfiles
{
    internal class AuthenticationProfile:Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
