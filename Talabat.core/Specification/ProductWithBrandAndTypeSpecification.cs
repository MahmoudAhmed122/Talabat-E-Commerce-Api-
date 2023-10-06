using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specification
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecification<Product>
    {


        public ProductWithBrandAndTypeSpecification(ProductParameters parameters) : base( // GetAllProducts
            p => (
            (string.IsNullOrEmpty(parameters.Search)||p.Name.ToLower().Contains(parameters.Search))&&
            (!parameters.TypeId.HasValue || p.ProductTypeId == parameters.TypeId) &&
            (!parameters.BrandId.HasValue || p.ProductBrandId == parameters.BrandId)
            ))
            
        // where(p=>p.brandId==brandId)
        // .Include(p=>p.ProductType)
        // .Include(p=>p.ProductBrand)
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
            AddOrderBy(p => p.Name);

            if (parameters.Sort != null)
            {
                switch (parameters.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;

                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;

                    default:
                        AddOrderBy(p => p.Name);
                        break;

                }

            }

            ApplyPagination(parameters.PageSize *(parameters.PageIndex-1), parameters.PageSize);

                //products   18
                //PageSize    5
                //PageIndex   2 
        }

        public ProductWithBrandAndTypeSpecification(int id) : base(p => p.Id == id)//Contains Where()
        {

            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);

        }

    }
}
