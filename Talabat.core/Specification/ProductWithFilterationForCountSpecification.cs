using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specification
{
    public class ProductWithFilterationForCountSpecification:BaseSpecification<Product>
    {
        public ProductWithFilterationForCountSpecification(ProductParameters parameters)
            : base( 
            p => (
            (string.IsNullOrEmpty(parameters.Search) || p.Name.ToLower().Contains(parameters.Search)) &&
            (!parameters.TypeId.HasValue || p.ProductTypeId == parameters.TypeId) &&
            (!parameters.BrandId.HasValue || p.ProductBrandId == parameters.BrandId)
            ))
            
        {
                
        }

    }
}
