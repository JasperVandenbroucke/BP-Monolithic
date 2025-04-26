using AutoMapper;
using ECommerceApi.Models.Dtos.ShoppingCarts;
using ECommerceApi.Services.ShoppingCarts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace ECommerceApi.Controllers.ShoppingCarts
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingCartsController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IMapper _mapper;

        public ShoppingCartsController(IShoppingCartService shoppingCartService, IMapper mapper)
        {
            _shoppingCartService = shoppingCartService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ShoppingCartReadDto>> GetCart()
        {
            Console.WriteLine("--> Getting a shopping cart ...");
            Console.WriteLine($"--> UserId: {User.FindFirst(ClaimTypes.NameIdentifier)?.Value}");
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                return Unauthorized("UserId kon niet worden bepaald uit JWT token");

            var cart = await _shoppingCartService.GetCart(userId);
            if (cart == null)
                return NotFound($"Shopping cart not found for user {userId}");

            return Ok(_mapper.Map<ShoppingCartReadDto>(cart));
        }
    }
}
