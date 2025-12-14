using AutoMapper;
using ECommerce.Domain.Entity.IdentityModule;
using ECommerce.Services.Abstraction;
using ECommerce.Shared.CommonResponse;
using ECommerce.Shared.Dtos.IdentityDtos;
using ECommerce.Shared.Dtos.OrderDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthenticationService(UserManager<ApplicationUser> userManager, IConfiguration configuration , IMapper mapper)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto)
        {
            var userEmail = await _userManager.FindByEmailAsync(loginDto.email);
            if (userEmail is null)
                return Error.InvalidCredentials("User.InvalidCredintals");

            var IsPasswordValid = await _userManager.CheckPasswordAsync(userEmail, loginDto.password);
            if (!IsPasswordValid)
                return Error.InvalidCredentials("User.InvalidCredintals");

            var token = await CreateTokenAsync(userEmail);

            return new UserDto(userEmail.Email!, userEmail.DisplayName, token);
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                Email = registerDto.email,
                DisplayName = registerDto.displayName,
                PhoneNumber = registerDto.phoneNumber,
                UserName = registerDto.username
            };

            var Result = await _userManager.CreateAsync(user, registerDto.password);
            var token = await CreateTokenAsync(user);
            if (Result.Succeeded)
                return new UserDto(user.Email, user.DisplayName, token);

            return Result.Errors.Select(e => Error.Validation(e.Code, e.Description)).ToList();
        }

        public async Task<bool> CheckEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }

        public async Task<Result<UserDto>> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                return Error.NotFound("User Not Found", $"User With This Email: {email} Was Not Found");
            return new UserDto(user.Email!, user.DisplayName, await CreateTokenAsync(user));
        }

        public async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email , user.Email!),
                new Claim(JwtRegisteredClaimNames.Name , user.UserName!)
            };


            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var securityKey = _configuration["JWTOptions:SecretKey"]!;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(

                issuer: _configuration["JWTOptions:Issuer"],
                audience: _configuration["JWTOptions:Audience"],
                expires: DateTime.Now.AddHours(2),
                claims: claims,
                signingCredentials: cred

                );

            return new JwtSecurityTokenHandler().WriteToken(token);
           

        }

        public async Task<Result<AddressDto>> GetUserAddressAsync(string email)
        {
            var user = await _userManager.Users.Include(x=>x.Address).FirstOrDefaultAsync(x=>x.Email == email);

            if (user is null)
                return Error.NotFound("User Not Found", $"User With This Email {email} Is Not Found");

            if (user.Address is null)
                return Error.NotFound("Address Not Found", $"Address For This Email {email} Is Not Found");

            return _mapper.Map<AddressDto>(user.Address);




        }

        public async Task<Result<AddressDto>> UpdateUserAddressAsync(string email, AddressDto addressDto)
        {
            var user = await _userManager.Users.Include(x => x.Address).FirstOrDefaultAsync(x => x.Email == email);

            if (user is null)
                return Error.NotFound("User Not Found", $"User With This Email {email} Is Not Found");


            if (user.Address is not null) //update
            {
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
                user.Address.Street = addressDto.Street;
                user.Address.City = addressDto.City;
                user.Address.Country = addressDto.Country;                    
            }

            if (user.Address is null) //create
            {
                user.Address=_mapper.Map<Address>(addressDto);
            }

            await _userManager.UpdateAsync(user);
            return _mapper.Map<AddressDto>(user.Address);


        }
    }
}
