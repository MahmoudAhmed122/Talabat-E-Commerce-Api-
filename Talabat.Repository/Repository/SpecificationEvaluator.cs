using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specification;

namespace Talabat.Repository.Repository
{
    public static class SpecificationEvaluator<T> where T :BaseEntity
    {


        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var query = inputQuery; //query = context.Products

            if (spec.Criteria is not null) //query = context.Products.Where(P => p.Id == id)
                query = query.Where(spec.Criteria);
            
            if(spec.OrderBy!=null)
                query = query.OrderBy(spec.OrderBy);

            if (spec.OrderByDescending != null)
                query = query.OrderByDescending(spec.OrderByDescending);

            if (spec.IsPagenationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);

            query = spec.Includes.Aggregate(query,(currentQuery,includeExpression)=>currentQuery.Include(includeExpression));

            return query;   
        
        }
    }
}

// var query = context.Products.Where(P=>p.Id==id).Include(P=>P.ProductBrand).Include(P=>P.ProductType);