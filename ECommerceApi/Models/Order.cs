using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Models
{
    public class Order
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public List<OrderItem> Items { get; set; }

        [Required]
        public double TotalPrice { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
