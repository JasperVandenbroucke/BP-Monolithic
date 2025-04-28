using ECommerceApi.Data;
using ECommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Services.Orders
{
    public class OrderService(AppDbContext context) : IOrderService
    {
        private readonly AppDbContext _context = context;

        public async Task<Order> GetOrderById(int orderId, int userId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);
        }

        public async Task PlaceOrder(Order orderToPlace)
        {
            _context.Orders.Add(orderToPlace);
            await _context.SaveChangesAsync();
        }
    }
}
