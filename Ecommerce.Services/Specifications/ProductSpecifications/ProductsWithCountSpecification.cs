using ECommerce.Domain.Entity;
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
    public class ProductsWithCountSpecification : BaseSpecification<Product, int>
    {
        public ProductsWithCountSpecification(ProductQueryParam queryParam): base(ProductSpecificationHelper.GetCriteria(queryParam))
        {


        }
    }
}
