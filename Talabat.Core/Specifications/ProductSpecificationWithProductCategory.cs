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
        public ProductSpecificationWithProductCategory() : base()
        {
            addIncludes();
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
