using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Mappings;
using Talabat.Core.IGenericRepository;
using Talabat.Repository.Repository;

namespace Talabat.APIs.Extensions
{
    public  static class SwaggerServices
    {


        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
         services.AddEndpointsApiExplorer();
         services.AddSwaggerGen();

            return services;

        }
    }
}
