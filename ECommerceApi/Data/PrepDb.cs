using ECommerceApi.Auth;
using ECommerceApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Data
{
    public class PrepDb
    {
        public static async Task PrepPopulation(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            await SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), serviceScope.ServiceProvider.GetService<PasswordHasher>());
        }

        private static async Task SeedData(AppDbContext context, PasswordHasher passwordHasher)
        {
            //Console.WriteLine("--> Attempting to apply migrations...");
            //try
            //{
            //    context.Database.Migrate();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
            //    return;
            //}

            if (!context.Products.Any())
                await SeedProducts(context);
            if (!context.Users.Any())
                await SeedUsers(context, passwordHasher);
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

        private static async Task SeedUsers(AppDbContext context, PasswordHasher passwordHasher)
        {
            Console.WriteLine("--> Seeding users...");
            context.Users.AddRange(
                new User() { Username = "test", PasswordHash = passwordHasher.HashPassword("plopkoek") }
            );
            await context.SaveChangesAsync();
        }
    }
}