using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entity.ProductModule
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public decimal Price { get; set; }

        #region Relations

        #region Product - Brand (1 , m)

        public int ProductBrandId { get; set; }
        public ProductBrand ProductBrand { get; set; } = null!;


        #endregion

        #region Product - ProductType (1 , m)

        public ProductType ProductType { get; set; } = null!;
        public int ProductTypeId { get; set; }
        #endregion

        #endregion

    }
}
