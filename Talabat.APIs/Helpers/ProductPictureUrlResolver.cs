using AutoMapper;
using Talabat.Core.Entities;
using Talabat.APIs.DTOs;
namespace Talabat.APIs.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        public IConfiguration Configuration { get; }

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl)) 

                return $"{Configuration["ApiBaseUrl"]}{source.PictureUrl}";
            
            return null;

        }
    }
}
