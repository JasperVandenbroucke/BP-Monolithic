using ECommerceApi.Data;
using ECommerceApi.Models;
using ECommerceApi.Services.Products;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Services.ShoppingCarts
{
    public class ShoppingCartService(AppDbContext context, IProductService productService) : IShoppingCartService
    {
        private readonly AppDbContext _context = context;
        public readonly IProductService _productService = productService;

        public async Task<ShoppingCart> GetCart(int userId)
        {
            var cart = await _context.ShoppingCarts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null) return null;

            var productIds = cart.Items.Select(i => i.ProductId).Distinct().ToList();
            var products = await _productService.GetProductsByIds(productIds);
            foreach (var item in cart.Items)
            {
                var product = products.FirstOrDefault(p => p.Id == item.ProductId);
                if (product != null)
                    item.Product = product;
            }

            return cart;
        }

        public async Task AddProductToCart(int userId, int productId)
        {
            if (!await _context.Products.AnyAsync(p => p.Id == productId))
                throw new Exception($"No product found with id {productId}");

            var cart = await GetCart(userId);
            if (cart == null)
            {
                cart = new ShoppingCart { UserId = userId };
                _context.ShoppingCarts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var cartItem = new ShoppingCartItem
            {
                ShoppingCartId = cart.Id,
                ProductId = productId
            };
            _context.ShoppingCartItems.Add(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCartOfUser(int userId)
        {
            var cart = await _context.ShoppingCarts.FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart != null)
            {
                _context.ShoppingCarts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }
    }
}
