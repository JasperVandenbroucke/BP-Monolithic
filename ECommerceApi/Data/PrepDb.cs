using ECommerceApi.Models;

namespace ECommerceApi.Data
{
    public class PrepDb
    {
        public static async Task PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            await SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
        }

        private static async Task SeedData(AppDbContext context, bool isProd)
        {
            //if (isProd)
            //{
            //    Console.WriteLine("--> Attempting to apply migrations...");
            //    try
            //    {
            //        context.Database.Migrate();
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine($"--> Could not run migrations: {ex.Message}");
            //    }
            //}

            if (!context.Products.Any())
                await SeedProducts(context);
            else
                Console.WriteLine("--> We already have data");
        }

        private static async Task SeedProducts(AppDbContext context)
        {
            Console.WriteLine("--> Seeding products...");
            context.Products.AddRange(
                new Product() { Name = "Hoodie", Price = 49.99 },
                new Product() { Name = "Polo", Price = 24.99 },
                new Product() { Name = "Blouse", Price = 29.99 },
                new Product() { Name = "Jeans", Price = 59.99 },
                new Product() { Name = "Short", Price = 24.99 },
                new Product() { Name = "Rok", Price = 34.99 }
            );
            await context.SaveChangesAsync();
        }
    }
}