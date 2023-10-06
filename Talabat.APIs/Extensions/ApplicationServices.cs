using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Mappings;
using Talabat.core.IRepositories;
using Talabat.core.Services;
using Talabat.Core.IGenericRepository;
using Talabat.Repository;
using Talabat.Repository.Repository;
using Talabat.Service.OrderServices;
using Talabat.Service.PaymentServices;
using Talabat.Service.Tokens;

namespace Talabat.APIs.Extensions
{
    public static class ApplicationServices  
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services) {

           services.AddAutoMapper(typeof(MappingProfile));
           services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork , UnitOfWork>();
            services.AddScoped<IPayementService, PaymentService>();

            services.AddScoped<IOrderService, OrderServices>();
            #region Customize Validation Error Response

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)

                                                        .SelectMany(p => p.Value.Errors)
                                                        .Select(p => p.ErrorMessage)
                                                        .ToArray();
                    // each parameter have key value pair 
                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(validationErrorResponse);
                };

            });
            #endregion
            return services;

        
        }

    }
}
