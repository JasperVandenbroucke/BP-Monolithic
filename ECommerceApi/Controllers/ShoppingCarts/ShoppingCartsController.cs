using AutoMapper;
using ECommerceApi.Models.Dtos.ShoppingCarts;
using ECommerceApi.Services.Products;
using ECommerceApi.Services.ShoppingCarts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceApi.Controllers.ShoppingCarts
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingCartsController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ShoppingCartsController(IShoppingCartService shoppingCartService, IProductService productService, IMapper mapper)
        {
            _shoppingCartService = shoppingCartService;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ShoppingCartReadDto>> GetCart()
        {
            Console.WriteLine("--> Getting a shopping cart...");
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                return Unauthorized("UserId kon niet worden bepaald uit JWT token");

            var cart = await _shoppingCartService.GetCart(userId);
            if (cart == null)
                return NotFound($"Shopping cart not found for user {userId}");

            return Ok(_mapper.Map<ShoppingCartReadDto>(cart));
        }

        [HttpPut("{productId}")]
        public async Task<ActionResult> AddProductToCart(int productId)
        {
            Console.WriteLine("--> Adding a product...");
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                return Unauthorized("UserId kon niet worden bepaald uit JWT token");

            try
            {
                await _shoppingCartService.AddProductToCart(userId, productId);
                return Ok($"Product {productId} added to cart for user {userId}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add product {productId} to cart of user {userId}: {ex.Message}");
            }
        }
    }
}
