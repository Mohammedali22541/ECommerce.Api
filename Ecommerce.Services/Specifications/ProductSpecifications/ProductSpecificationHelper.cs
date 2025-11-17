using ECommerce.Domain.Entity.ProductModule;
using ECommerce.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Specifications.ProductSpecifications
{
    internal static class ProductSpecificationHelper
    {
        public static Expression<Func<Product , bool>> GetCriteria (ProductQueryParam queryParam)
        {
            return b => (!queryParam.BrandId.HasValue || b.ProductBrandId == queryParam.BrandId.Value) && (!queryParam.TypeId
          .HasValue || b.ProductTypeId == queryParam.TypeId.Value) && (string.IsNullOrEmpty(queryParam.Search) || b.Name.ToLower().Contains(queryParam.Search));
            }
    }
}
