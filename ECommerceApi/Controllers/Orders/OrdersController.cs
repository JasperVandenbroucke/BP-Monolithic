using AutoMapper;
using ECommerceApi.Models;
using ECommerceApi.Models.Dtos.Orders;
using ECommerceApi.Models.Dtos.ShoppingCarts;
using ECommerceApi.Services.Orders;
using ECommerceApi.Services.ShoppingCarts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceApi.Controllers.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IShoppingCartService shoppingCartService, IMapper mapper)
        {
            _orderService = orderService;
            _shoppingCartService = shoppingCartService;
            _mapper = mapper;
        }

        [HttpGet("{orderId}", Name = "GetOrderById")]
        public async Task<ActionResult<OrderReadDto>> GetOrderById(int orderId)
        {
            Console.WriteLine("--> Getting an order...");
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                return Unauthorized("UserId kon niet worden bepaald uit JWT token");

            try
            {
                var order = await _orderService.GetOrderById(orderId, userId) ?? throw new Exception($"No order found with id {orderId} for user {userId}");
                return Ok(_mapper.Map<OrderReadDto>(order));
            }
            catch (Exception ex)
            {
                return NotFound($"Failed to get the order {orderId}: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrderReadDto>> CreateOrder()
        {
            try
            {
                Console.WriteLine("--> Placing an order...");
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                    return Unauthorized("UserId kon niet worden bepaald uit JWT token");

                var cart = await _shoppingCartService.GetCart(userId);
                if (cart is null) return NotFound($"ShoppingCart not found for user {userId}");
                var cartDto = _mapper.Map<ShoppingCartReadDto>(cart);
                var orderToPlace = _mapper.Map<Order>(cartDto);
                orderToPlace.Status = "Verwerking";
                orderToPlace.TotalPrice = orderToPlace.Items.Sum(i => i.Price);

                await _orderService.PlaceOrder(orderToPlace);
                await _shoppingCartService.RemoveCartOfUser(userId);

                var orderReadDto = _mapper.Map<OrderReadDto>(orderToPlace);
                return CreatedAtRoute(nameof(GetOrderById), new { OrderId = orderReadDto.Id }, orderReadDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
