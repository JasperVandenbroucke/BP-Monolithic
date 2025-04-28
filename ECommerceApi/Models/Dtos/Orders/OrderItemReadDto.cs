namespace ECommerceApi.Models.Dtos.Orders
{
    public class OrderItemReadDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
