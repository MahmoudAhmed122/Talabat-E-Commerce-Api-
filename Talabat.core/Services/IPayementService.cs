using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.core.Services
{
    public interface IPayementService
    {

        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);

    }
}
