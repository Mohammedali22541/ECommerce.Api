using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entity.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.IdentityData.DataSeed
{
    public class IdentityDataIntializer : IDataIntializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger _logger;

        public IdentityDataIntializer(UserManager<ApplicationUser> userManager , RoleManager<IdentityRole> roleManager , ILogger<IdentityDataIntializer> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        public async Task IntializeAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }
                if (!_userManager.Users.Any())
                {
                    var User1 = new ApplicationUser
                    {
                        DisplayName = "Mohammed Ali",
                        Email = "Mohammaedali792406@gmail.com",
                        UserName = "Mohammedali",
                        PhoneNumber = "01091945839"
                    };
                    var User2 = new ApplicationUser
                    {
                        DisplayName = "Mo Ali",
                        Email = "Moali792406@gmail.com",
                        UserName = "Moali",
                        PhoneNumber = "01091945838"
                    };

                    await _userManager.CreateAsync(User1, "P@ssw0rd");
                    await _userManager.CreateAsync(User2, "P@ssw0rd");

                    await _userManager.AddToRoleAsync(User1, "SuperAdmin");
                    await _userManager.AddToRoleAsync(User2, "Admin");
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error While Seeding Database , {ex.Message} Happend");
            }
        }
    }
}
