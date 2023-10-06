using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }

        public  List<Expression<Func<T, object>>>Includes { get; set; }
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDescending { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPagenationEnabled { get; set; }

        public BaseSpecification()   // If the Query does not have Where()
        {
            Includes = new List<Expression<Func<T, object>>>();
 

        }

        public void AddOrderBy(Expression<Func<T, object>> orderBy) { 
        
        OrderBy=orderBy;
        }
        public void AddOrderByDescending(Expression<Func<T, object>> orderByDesc)
        {

            OrderByDescending = orderByDesc;
        }

        public void ApplyPagination(int skip , int take) {

            IsPagenationEnabled = true;
            Take=take;
            Skip = skip;
        }
        
        public BaseSpecification(Expression<Func<T, bool>> criteriaExpression)   // If the Query have Where()
        {
            Criteria = criteriaExpression;
            Includes = new List<Expression<Func<T, object>>>();

        }




    }
}
