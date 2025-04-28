using ECommerceApi.Models;

namespace ECommerceApi.Services.Orders
{
    public interface IOrderService
    {
        Task<Order> GetOrderById(int orderId, int userId);
        Task PlaceOrder(Order orderToPlace);
    }
}
