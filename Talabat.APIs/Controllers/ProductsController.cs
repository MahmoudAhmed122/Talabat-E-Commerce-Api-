using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.core.IRepositories;
using Talabat.Core.Entities;
using Talabat.Core.IGenericRepository;
using Talabat.Core.Specification;

namespace Talabat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public ProductsController(IMapper mapper , IUnitOfWork unitOfWork )
        {
            
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetAllProducts([FromQuery] ProductParameters parameters)
        {
            var spec = new ProductWithBrandAndTypeSpecification(parameters);
            var products = await unitOfWork.Repository<Product>().GetAllAsyncWithSpecification(spec);
            var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            var countSpecification = new ProductWithFilterationForCountSpecification(parameters);
            var count = await unitOfWork.Repository<Product>().CountWithSpecificationAsync(countSpecification);
            return Ok(new Pagination<ProductToReturnDto>(parameters.PageSize,parameters.PageIndex , count, data));
        }

        //Size  Index    Count (n o data after felteration )   data 
        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {

            var spec = new ProductWithBrandAndTypeSpecification(id);
            var product = await unitOfWork.Repository<Product>().GetByIdAsyncWithSpecification(spec);
            if (product is null)
                return NotFound(new ApiResponse(404));
            var ProductDto = mapper.Map<Product, ProductToReturnDto>(product);
            return Ok(ProductDto);
        }   

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {

            var brands = await unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return Ok(brands);

        }
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetAllTypes()
        {

            var types = await unitOfWork.Repository<ProductType>().GetAllAsync();
            return Ok(types);

        }
    }
}
