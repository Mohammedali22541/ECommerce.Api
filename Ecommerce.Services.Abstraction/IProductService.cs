using ECommerce.Shared.Dtos;
using ECommerce.Shared.Dtos.ProductsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Abstraction
{
    public interface IProductService
    {
        Task<IEnumerable<productDto>> GetAllProductsAsync(ProductQueryParam queryParam);
        Task<productDto> GetProductByIdAsync(int id);

        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();


    }
}
