using ECommerce.Services.Abstraction;
using ECommerce.Shared.Dtos;
using ECommerce.Shared.Dtos.ProductsDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable< productDto>>> GeTAllProductAsync( [FromQuery]ProductQueryParam queryParam)
        {
            var AllProduct = await _productService.GetAllProductsAsync( queryParam);
            return Ok(AllProduct);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<productDto>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<BrandDto>> GetAllBrands()
        {
            var allBrands = await _productService.GetAllBrandsAsync();
            return Ok(allBrands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<TypeDto>> GetAllTypes()
        {
            var allTypes = await _productService.GetAllTypesAsync();
            return Ok(allTypes);
        }
    }
}
