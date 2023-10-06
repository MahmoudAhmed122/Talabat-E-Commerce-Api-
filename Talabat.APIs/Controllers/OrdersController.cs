using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.core.Entities.Order_Aggregate;
using Talabat.core.Services;
using Talabat.Repository;

namespace Talabat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        public IOrderService orderService { get; }
        public IMapper mapper { get; }

        public OrdersController(IOrderService orderService , IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        [ProducesResponseType(typeof(Order) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto) {

            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var shippingAddress = mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
          var order= await orderService.CreateOrderAsync(buyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, shippingAddress);
            if (order is null)
                return BadRequest(new ApiResponse(400));
            return Ok(mapper.Map<Order, OrderToReturnDto>(order));

        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForSpecificUser()
        {

            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var orders = await orderService.GetOrdersForUserAsync(buyerEmail);
            return Ok(mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders));  
        }

        [HttpGet("GetOrderById")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForSpecificUser(int id) {

            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var order = await orderService.GetOrderByIdForUserAsync(id , buyerEmail);
            if(order is null)
                return NotFound(new ApiResponse(404));
            return Ok(mapper.Map<Order, OrderToReturnDto>(order));

        }
        [HttpGet("GetDeliveryMethods")]
        public async Task<ActionResult<DeliveryMethod>> GetDeliveryMethods()
        {
            var deliveryMethods = await orderService.GetDeliveryMethodsAsync();
          
            return Ok(deliveryMethods);

        }

    }
}
