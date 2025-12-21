using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecs
{
    public class ProductSpecificationWithProductCategory : BaseSpecifications<Product>
    {
        public ProductSpecificationWithProductCategory(ProductSpecParams specParams) 
            : base(p =>
                            (!specParams.BrandId.HasValue|| p.BrandId == specParams.BrandId.Value  )&&
                            (!specParams.CategoryId.HasValue || p.CategoryId == specParams.CategoryId.Value)
        
        )
        {
            addIncludes();

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Price);
                        break;
                }

            }
            else
            {
                AddOrderBy(p => p.Name);
            }
        }

       

        public ProductSpecificationWithProductCategory(int id) : base(p=>p.Id==id)
        {
            addIncludes();
        }

        private void addIncludes()
        {
            Inculdes.Add(p => p.Brand);
            Inculdes.Add(p => p.Category);
        }
    }
}
