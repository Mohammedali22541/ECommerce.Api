using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entity.ProductModule;
using ECommerce.Services.Abstraction;
using ECommerce.Shared.Dtos.ProductsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class ProductServices : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<productDto>> GetAllProductsAsync()
        {
            var AllProduct =await _unitOfWork.GetRepository<Product, int>().GetAllAsync();

            return _mapper.Map<IEnumerable<Product>, IEnumerable<productDto>>(AllProduct);
        }

        public async Task<productDto> GetProductByIdAsync(int id)
        {
           var Product = await _unitOfWork.GetRepository<Product , int>().GetByIdAsync(id);

            return _mapper.Map<productDto>(Product);

        }

        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var AllBrands=await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(AllBrands);
        }



        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var AllTypes = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();

            return _mapper.Map<IEnumerable< ProductType>,IEnumerable< TypeDto>>(AllTypes);
        }

      
    }
}
