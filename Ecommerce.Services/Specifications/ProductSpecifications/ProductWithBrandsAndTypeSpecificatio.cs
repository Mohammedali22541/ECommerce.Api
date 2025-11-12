using ECommerce.Domain.Entity.ProductModule;
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
        :base(b=>(!queryParam.BrandId.HasValue || b.ProductBrandId == queryParam.BrandId.Value) && (!queryParam.TypeId
        .HasValue || b.ProductTypeId == queryParam.TypeId.Value) && (string.IsNullOrEmpty(queryParam.Search) ||  b.Name.ToLower().Contains(queryParam.Search)))  
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p=>p.ProductType);

        }
        public ProductWithBrandsAndTypeSpecificatio(int id):base(x=>x.Id == id) 
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }

    }
}
