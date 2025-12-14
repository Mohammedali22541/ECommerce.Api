using ECommerce.Services.Abstraction;
using ECommerce.Shared.CommonResponse;
using ECommerce.Shared.Dtos.OrderDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presentation.Controllers
{
    public class OrdersController : ApiBaseController 
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var result = await _orderService.CreateOrderAsync(orderDto , GetEmailFromToken());  
            return HandleResult(result);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetOrders()
        {
            var result = await _orderService.GetAllOrderAsync(GetEmailFromToken());
            return HandleResult(result);
        }
        [Authorize]
        [HttpGet ("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDto>>GetOrder(Guid id)
        {
            var result = await _orderService.GetOrderByIdAsync(GetEmailFromToken(),id);
            return HandleResult(result);
        }

        [AllowAnonymous]
        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            var result = await _orderService.GetAllDeliveryMethodAsync();
            return HandleResult(result);
        }

    }
}
