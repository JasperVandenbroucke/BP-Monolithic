using ECommerceApi.Models;

namespace ECommerceApi.Services.Products
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
    }
}
