using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecs
{
    public  class ProductsWithFilteraionForCountSpecification : BaseSpecifications<Product>
    {
        public ProductsWithFilteraionForCountSpecification(ProductSpecParams specParams)
            :base
            (
                 p =>
                            (!specParams.BrandId.HasValue || p.BrandId == specParams.BrandId.Value) &&
                            (!specParams.CategoryId.HasValue || p.CategoryId == specParams.CategoryId.Value)

                 )
        {

        }
    }
}
