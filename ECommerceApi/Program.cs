using ECommerceApi.Auth;
using ECommerceApi.Data;
using ECommerceApi.Services.Products;
using ECommerceApi.Services.ShoppingCarts;
using ECommerceApi.Services.Users;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Use an InMemory db
Console.WriteLine("--> Using an InMemory db");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ECommerceDatabase")
);

builder.Services.AddSingleton<PasswordHasher>();
builder.Services.AddSingleton<JwtTokenProvider>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

await PrepDb.PrepPopulation(app, app.Environment.IsProduction());

app.Run();
