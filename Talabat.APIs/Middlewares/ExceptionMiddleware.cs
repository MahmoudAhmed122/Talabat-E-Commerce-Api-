using System.Text.Json;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment environment;

        public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger , IHostEnvironment environment )
        {
            this.next = next;
            this.logger = logger;
            this.environment = environment;
        }

        public async Task InvokeAsync(HttpContext context ) {
            // Context is the context of the request
            try
            {
                await next.Invoke(context);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                // will log the exception in the database
                 context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

                var response =
                    environment.IsDevelopment() ?
                    new ApiExceptionResponse(500, ex.Message, ex.StackTrace.ToString())
                :
                new ApiExceptionResponse(500);
                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
              var jsonResponse= JsonSerializer.Serialize(response , options);
                context.Response.WriteAsync(jsonResponse);  

            }
        }
    }
}
