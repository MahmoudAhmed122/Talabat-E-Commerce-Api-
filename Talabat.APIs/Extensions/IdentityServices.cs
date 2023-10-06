using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.core.Entities.Identity;
using Talabat.core.Services;
using Talabat.Repository.Identity;
using Talabat.Service.Tokens;

namespace Talabat.APIs.Extensions
{
    public  static class IdentityServices
    {


        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddIdentity<ApplicationUser, IdentityRole>(options => {

                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
            })
             .AddEntityFrameworkStores<IdentityStoreContext>();
            services.AddAuthentication(options => { 
            
                options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;   
                options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;  
            }
            )
                .AddJwtBearer(options =>{
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Jwt:ValidIssuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["Jwt:ValidAudience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                
                });   // to allow dependecy injection for UserManager

            return services;
        }
    }
}
   