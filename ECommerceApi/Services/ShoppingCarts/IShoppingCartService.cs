using ECommerceApi.Models;

namespace ECommerceApi.Services.ShoppingCarts
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> GetCart(int userId);
        Task AddProductToCart(int userId, int productId);
    }
}
