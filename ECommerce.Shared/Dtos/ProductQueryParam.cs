using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Dtos
{
    public class ProductQueryParam
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Search { get; set; } 
    }
}
