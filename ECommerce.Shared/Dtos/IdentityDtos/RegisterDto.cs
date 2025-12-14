using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Dtos.IdentityDtos
{
    public record RegisterDto([EmailAddress]string email ,string displayName ,  string username , string password , [Phone] string phoneNumber );
    
}
