using ECommerceApi.Models;

namespace ECommerceApi.Services.Products
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<List<Product>> GetProductsByIds(List<int> ids);
        Task<bool> DoesProductExist(int productId);
    }
}
