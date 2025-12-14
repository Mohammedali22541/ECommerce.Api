using ECommerce.Shared.CommonResponse;
using ECommerce.Shared.Dtos.IdentityDtos;
using ECommerce.Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Abstraction
{
    public interface IAuthenticationService
    {
        Task <Result<UserDto>> LoginAsync (LoginDto loginDto);
        Task<Result<UserDto>> RegisterAsync (RegisterDto registerDto);
        Task<bool> CheckEmailAsync(string email);
        Task<Result<UserDto>> GetUserByEmailAsync (string email);

        Task<Result<AddressDto>> GetUserAddressAsync(string email);
        Task<Result<AddressDto>> UpdateUserAddressAsync(string email, AddressDto addressDto);
    }
}
