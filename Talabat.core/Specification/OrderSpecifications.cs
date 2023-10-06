using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities.Order_Aggregate;
using Talabat.Core.Specification;

namespace Talabat.core.Specification
{
    public class OrderSpecifications:BaseSpecification<Order>
    {
        public OrderSpecifications(string buyerEmail)
            :base(O=>O.BuyerEmail== buyerEmail)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);

            AddOrderByDescending(o => o.OrderDate);
        }
        public OrderSpecifications(string buyerEmail , int id)
          : base(O => O.BuyerEmail == buyerEmail && O.Id==id)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);

        }

    }
}
