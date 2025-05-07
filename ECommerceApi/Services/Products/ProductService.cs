using ECommerceApi.Data;
using ECommerceApi.Models;
using ECommerceApi.Models.Dtos.Products;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Services.Products
{
    public class ProductService(AppDbContext context) : IProductService
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetProductsByIds(List<int> ids)
        {
            return await _context.Products
                .Where(p => ids.Contains(p.Id))
                .ToListAsync();
        }

        public async Task<bool> DoesProductExist(int productId)
        {
            return await _context.Products.AnyAsync(p => p.Id == productId);
        }

        public async Task CreateProduct(ProductCreateDto productCreateDto)
        {
            _context.Products.Add(new Product() { Name = productCreateDto.Name, Price = productCreateDto.Price });
            await _context.SaveChangesAsync();
        }
    }
}
