using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Dtos
{
    public class ProductQueryParam
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Search { get; set; }
        public ProductSortingOptions Sort { get; set; }




        private int _pageIndex = 1;
        public int PageIndex
        {
            get { return _pageIndex; }
            set
            {
                _pageIndex = (value <= 0) ? 1 : value;
            }
        }


        private const int _defaultPageSize = 5;
        private const int _maxPageSize = 10;


        private int _pageSize = _defaultPageSize;

        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                if (value <= 0)
                {
                    _pageSize = _defaultPageSize;
                }
                else if (value >= 10)
                {
                    _pageSize = _maxPageSize;
                }
                else
                {
                    _pageSize = value;
                }
            }
        }


    }
}
