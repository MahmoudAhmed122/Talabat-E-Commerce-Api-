using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specification
{
    public interface ISpecification<T> where T : BaseEntity   
    { 
        public Expression<Func<T,bool>> Criteria { get; set; } // Where(P=>p.Id==id)

        public Expression<Func<T, object>> OrderBy { get; set; } // OrderBy(P=>p.Name)

        public Expression<Func<T, object>> OrderByDescending { get; set; } // OrderBy(P=>p.Name)

        public List<Expression<Func<T,object>>> Includes { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }

        public bool IsPagenationEnabled { get; set; }
    }
}


// In this interface we will put a property for each component in the query
// Context.Products.Where(P=>p.Id==id).Include(P=>p.ProductBrand).Include(P=>p.ProductType);