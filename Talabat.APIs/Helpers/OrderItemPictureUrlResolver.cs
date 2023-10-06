using AutoMapper;
using Talabat.Core.Entities;
using Talabat.APIs.DTOs;
using Talabat.core.Entities.Order_Aggregate;

namespace Talabat.APIs.Helpers
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {

        public IConfiguration Configuration { get; }

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))

                return $"{Configuration["ApiBaseUrl"]}{source.PictureUrl}";

            return null;
        }
    }
}
