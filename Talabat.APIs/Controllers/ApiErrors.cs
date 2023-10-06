using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Repository.Data;

namespace Talabat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiErrors : ControllerBase
    {
        public StoreContext Context { get; }

        public ApiErrors(StoreContext context)
        {
            Context = context;
        }
        [HttpGet("NotFound")]
        public ActionResult GetNotFoundError() {
            var product = Context.Products.Find(1000);
            if (product == null) 
                return NotFound(new ApiResponse(404));
            return Ok(product);

        }

        [HttpGet("ServerError")]
        public ActionResult GetServerError()
        {
            var product = Context.Products.Find(1000);
            var productToReturn = product.ToString();  // Null Reference Exception
            return Ok(productToReturn);

        }

        [HttpGet("BadRequest")]
        public ActionResult GetBadRequestError()
        {
            
            return BadRequest(new ApiResponse(400));

        }

        [HttpGet("BadRequest/{id}")]   // ValidationError
        public ActionResult GetBadRequestError(int id)
        {

            return Ok();

        }

    }
}
