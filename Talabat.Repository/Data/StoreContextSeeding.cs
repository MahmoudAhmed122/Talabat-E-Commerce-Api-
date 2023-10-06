using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.core.Entities.Order_Aggregate;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeeding
    {
        public static async Task SeedAsync(StoreContext context)
        {


            if (!context.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeeds/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands is not null && brands.Count > 0)
                {
                    foreach (var brand in brands)
                    {
                        await context.ProductBrands.AddAsync(brand);

                    }
                    await context.SaveChangesAsync();

                }
            }

            if (!context.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("../Talabat.Repository/Data/DataSeeds/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                if (types is not null && types.Count > 0)
                {
                    foreach (var type in types)
                    {
                        await context.ProductTypes.AddAsync(type);

                    }
                    await context.SaveChangesAsync();

                }
            }

            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText("../Talabat.Repository/Data/DataSeeds/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products is not null && products.Count > 0)
                {
                    foreach (var product in products)
                    {
                        await context.Products.AddAsync(product);

                    }
                    await context.SaveChangesAsync();

                }
            }

            if (!context.DeliveryMethods.Any())
            {
                var DeliveryMethodsData = File.ReadAllText("../Talabat.Repository/Data/DataSeeds/delivery.json");
                var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);

                if (DeliveryMethods is not null && DeliveryMethods.Count > 0)
                {
                    foreach (var DeliveryMethod in DeliveryMethods)
                    {
                        await context.DeliveryMethods.AddAsync(DeliveryMethod);

                    }
                    await context.SaveChangesAsync();

                }
            }
        }


    }
}
