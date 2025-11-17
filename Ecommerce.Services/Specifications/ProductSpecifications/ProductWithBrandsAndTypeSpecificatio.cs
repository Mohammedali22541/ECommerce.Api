using ECommerce.Domain.Entity.ProductModule;
using ECommerce.Shared;
using ECommerce.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Specifications.ProductSpecifications
{
    public class ProductWithBrandsAndTypeSpecificatio:BaseSpecification<Product , int>
    {
        public ProductWithBrandsAndTypeSpecificatio(ProductQueryParam queryParam) 
        :base(ProductSpecificationHelper.GetCriteria(queryParam))  
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p=>p.ProductType);

            switch (queryParam.Sort)
            {
                case ProductSortingOptions.NameASc:
                    AddOrderByAsc(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderByAsc(p => p.Price);
                    break;
                    case ProductSortingOptions.PriceDesc:
                    AddOrderByDesc(p => p.Price);
                    break;
                default:
                    AddOrderByAsc(p => p.Id);
                    break;
            }

            ApplyPagination(queryParam.PageIndex, queryParam.PageSize);
        }
        public ProductWithBrandsAndTypeSpecificatio(int id):base(x=>x.Id == id) 
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }




    }
}
