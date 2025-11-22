using ECommerce.Services.Abstraction;
using ECommerce.Shared.Dtos.BasketDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class BasketController : ControllerBase
    {
        private readonly IBasketServices _basketServices;

        public BasketController(IBasketServices basketServices)
        {
            _basketServices = basketServices;
        }

        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasketAsync(string id)
        {
            var basket = await _basketServices.GetBasketAsync(id);
            return Ok(basket);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasketAsync([FromRoute]string id)
        {
            var isDeleted = await _basketServices.DeleteBasketAsync(id);
            return Ok(isDeleted);

        }
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateAsync(BasketDto basketDto)
        {
            var basket =await _basketServices.CreateOrUpdateAsync(basketDto);
            return Ok(basket);
        }



    }
}
