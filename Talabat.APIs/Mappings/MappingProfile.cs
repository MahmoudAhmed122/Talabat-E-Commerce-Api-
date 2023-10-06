using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;
using Talabat.APIs.Helpers;
using System.Net.Sockets;
using Talabat.core.Entities.Identity;
using Talabat.core.Entities.Order_Aggregate;

namespace Talabat.APIs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Product,ProductToReturnDto>()
                     .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                     .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                     .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

            CreateMap<core.Entities.Identity.Address, AddressDto>()
                .ReverseMap();

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<AddressDto, core.Entities.Order_Aggregate.Address>();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
              .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost));
            CreateMap<OrderItem, OrderItemDto>()
             .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());
            ;

        }


    }
}
