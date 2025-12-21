using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductSpecificationWithProductCategory : BaseSpecifications<Product>
    {
        public ProductSpecificationWithProductCategory(string sort,int? brandId ,int? categoryId) 
            : base(p =>
                            (!brandId.HasValue|| p.BrandId == brandId.Value  )&&
                            (!categoryId.HasValue || p.CategoryId == categoryId.Value)
        
        )
        {
            addIncludes();

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
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
