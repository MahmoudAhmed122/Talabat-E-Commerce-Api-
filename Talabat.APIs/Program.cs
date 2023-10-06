using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Mappings;
using Talabat.APIs.Middlewares;
using Talabat.core.Entities.Identity;
using Talabat.Core.IGenericRepository;
using Talabat.Core.IRepositories;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Repository.Identity.Seeds;
using Talabat.Repository.Repository;
namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services 
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerServices();

            builder.Services.AddDbContext<StoreContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
            builder.Services.AddDbContext<IdentityStoreContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"))
           );

            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {

                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });

            builder.Services.AddSingleton(typeof(IBasketRepository), typeof(BasketRepository));

            builder.Services.AddApplicationServices();


            builder.Services.AddIdentityServices(builder.Configuration);

            var app = builder.Build();
            #endregion


            #region Update Database
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = services.GetRequiredService<StoreContext>();
                await dbContext.Database.MigrateAsync();

                var IdentityDbContext = services.GetRequiredService<IdentityStoreContext>();
                await IdentityDbContext.Database.MigrateAsync();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                await StoreContextSeeding.SeedAsync(dbContext);
                await IdentitySeeding.SeedUsers(userManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occur while applying migration");

            }

            #endregion


            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithRedirects("/errors/{0}");  // Not Found End Point Middleware

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
         
        }
    }
}