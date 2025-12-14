using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entity.BasketModule;
using ECommerce.Domain.Entity.OrderModule;
using ECommerce.Domain.Entity.ProductModule;
using ECommerce.Services.Abstraction;
using ECommerce.Services.Specifications.OrderSpecifications;
using ECommerce.Shared.CommonResponse;
using ECommerce.Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IMapper mapper, IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto, string email)
        {
            var orderAddress = _mapper.Map<AddressDto, OrderAddress>(orderDto.Address);

            var basket =await _basketRepository.GetBasketAsync(orderDto.BasketId);
            if (basket is null)
                return Error.NotFound("Basket Not Found",
                    $"Basket With This Id : {orderDto.BasketId} Was Not Found");

            var orderItems = new List<OrderItems>();

            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id);
                if (product is null)
                    return Error.NotFound("Product Not Found",
                   $"Product With This Id : {item.Id} Was Not Found");

                orderItems.Add(CreateOrderItem(item, product));
            }

            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethods , int>()
                .GetByIdAsync(orderDto.DeliveryMethodId);

            if (deliveryMethod is null)
                return Error.NotFound("Delivery Method Not Found",
                    $"Delivery Method With This Id : {orderDto.DeliveryMethodId} Was Not Found");

            var subTotal = orderItems.Sum(x=>x.Price * x.Quantity);

            var order = new Order()
            {
                Address = orderAddress,
                DeliveryMethods = deliveryMethod,
                SubTotal = subTotal,
                items = orderItems,
                UserEmail = email,
            };
             await _unitOfWork.GetRepository<Order,Guid>().AddAsync(order);
            var IsAdded = await _unitOfWork.SaveChangesAsync() > 0;
            if (!IsAdded)
                return Error.Failure("Order Failure", "There Was A Problem While Creating The Order");

            return _mapper.Map<OrderToReturnDto>(order);


            

            

        }

        private static OrderItems CreateOrderItem(BasketItem item, Product product)
        {
            return new OrderItems()
            {
                Product = new ProductItemOrder()
                {
                    ProductId = product.Id,
                    PictureUrl = product.PictureUrl,
                    ProductName = product.Name,
                },
                Price = product.Price,
                Quantity = item.Quantity,
            };
        }

        public async Task<Result<IEnumerable<DeliveryMethodDto>>> GetAllDeliveryMethodAsync()
        {
            var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethods, int>()
                .GetAllAsync();
            if (!deliveryMethods.Any())
                return Error.NotFound("Delivery Method Not Found", "No Delivery Method Founded");
            
               
            
            return Result<IEnumerable<DeliveryMethodDto>>.Ok(_mapper.Map<IEnumerable<DeliveryMethodDto>>(deliveryMethods));
        }

        public async Task<Result<IEnumerable<OrderToReturnDto>>> GetAllOrderAsync(string email)
        {
            var orderSpecification = new OrderSpecification(email);

            var Orders =await _unitOfWork.GetRepository<Order , Guid>().GetAllAsync(orderSpecification);
            if (!Orders.Any())
                return Error.NotFound("Orders Not Found", $"No Orders Founded For The User With Email: {email}");

            return Result<IEnumerable<OrderToReturnDto>>.Ok(_mapper.Map<IEnumerable<OrderToReturnDto>>(Orders));

        }

        public async Task<Result<OrderToReturnDto>> GetOrderByIdAsync(string email, Guid id)
        {
            var orderSpecification = new OrderSpecification(email, id);

            var order = await _unitOfWork.GetRepository<Order , Guid>().GetByIdAsync(orderSpecification);
            if (order is null)
                return Error.NotFound("Order Not Found", $"No Order Founded For The User With Email: {email}");

            return _mapper.Map<OrderToReturnDto>(order);



        }
    }
}
