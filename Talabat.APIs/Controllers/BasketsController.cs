using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;
using Talabat.APIs.Errors;
using AutoMapper;
using Talabat.APIs.DTOs;

namespace Talabat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        public IBasketRepository basketRepository { get; }
        public IMapper mapper { get; }

        public BasketsController(IBasketRepository basketRepository, IMapper mapper)
        {
            this.basketRepository = basketRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string Id)
        {

            var basket = await basketRepository.GetBasketAsync(Id);
            if (basket is null)
                return NotFound(new ApiResponse(404));
            return Ok(basket);
        }
        [HttpPost]

        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto _basketDto)
        {
            var _basket = mapper.Map<CustomerBasketDto, CustomerBasket>(_basketDto);
            var basket = await basketRepository.UpdateBasketAsync(_basket);

            if (basket is null)
                return BadRequest(new ApiResponse(400));

            return Ok(basket);
        }

        [HttpDelete]

        public async Task<ActionResult<bool>> DeleteBasket(string Id)
        {

            return await basketRepository.DeleteBasketAsync(Id);

        }



    }
}
