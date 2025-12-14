using ECommerce.Services.Abstraction;
using ECommerce.Shared.Dtos.IdentityDtos;
using ECommerce.Shared.Dtos.OrderDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presentation.Controllers
{
    public class AuthenticationController:ApiBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController( IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var result = await _authenticationService.LoginAsync(loginDto);

            return HandleResult(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var result =await  _authenticationService.RegisterAsync(registerDto);

            return HandleResult(result);
        }

        [HttpGet("emailExists")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var result = await _authenticationService.CheckEmailAsync(email);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            //var email = User.FindFirstValue(ClaimTypes.Email);
            var email = GetEmailFromToken();
            var result = await _authenticationService.GetUserByEmailAsync(email!);

            return HandleResult(result);
        } 

        [Authorize]
        [HttpGet("Address")]

        public async Task<ActionResult<AddressDto>> GetAddress()
        {
            var email = GetEmailFromToken();
            var result = await _authenticationService.GetUserAddressAsync(email);
            return HandleResult(result);
        }

        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto addressDto)
        {
            var email = GetEmailFromToken();
            var result = await _authenticationService.UpdateUserAddressAsync(email, addressDto);
            return HandleResult(result);
        }

    }
}
