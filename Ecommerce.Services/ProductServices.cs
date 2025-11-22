using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entity.ProductModule;
using ECommerce.Services.Abstraction;
using ECommerce.Services.Exceptions;
using ECommerce.Services.Specifications.ProductSpecifications;
using ECommerce.Shared;
using ECommerce.Shared.Dtos;
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
        public async Task<PaginationResult<productDto>> GetAllProductsAsync(ProductQueryParam queryParam)
        {
            var repo = _unitOfWork.GetRepository<Product, int>();
            var spec = new ProductWithBrandsAndTypeSpecificatio(queryParam);
            var AllProduct =await repo.GetAllAsync(spec);

            var data =  _mapper.Map<IEnumerable<Product>, IEnumerable<productDto>>(AllProduct);
            var pagesize = data.Count();

            var countSpec = new ProductsWithCountSpecification(queryParam);
            var totalCount = await repo.GetCountAsync(countSpec);

            return new PaginationResult<productDto>(queryParam.PageIndex, pagesize, totalCount, data);
        }

        public async Task<productDto> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandsAndTypeSpecificatio(id);
           var Product = await _unitOfWork.GetRepository<Product , int>().GetByIdAsync(spec);
            if (Product is null)
            {
                throw new ProductNotFoundException(id);
            }
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
