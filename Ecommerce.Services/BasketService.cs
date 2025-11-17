using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entity.BasketModule;
using ECommerce.Services.Abstraction;
using ECommerce.Shared.Dtos.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class BasketService : IBasketServices
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository , IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<BasketDto> CreateOrUpdateAsync(BasketDto createOrUpdateBasket)
        {
            var customerBasket = _mapper.Map<CustomerBasket>(createOrUpdateBasket);

            var createdOrUpdated =  await _basketRepository.CreateOrUpdateBasket(customerBasket);

            return _mapper.Map<BasketDto>(createdOrUpdated);
        }

        public async Task<bool> DeleteBasketAsync(string BasketId) => await _basketRepository.DeleteBasketAsync(BasketId);

        public async Task<BasketDto> GetBasketAsync(string BasketId)
        {
            var basket = await _basketRepository.GetBasketAsync(BasketId);

            return _mapper.Map<BasketDto>(basket);
        }
    }
}
