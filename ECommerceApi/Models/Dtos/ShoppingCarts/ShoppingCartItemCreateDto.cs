using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Models.Dtos.ShoppingCarts
{
    public class ShoppingCartItemCreateDto
    {
        [Required]
        public int ProductId { get; set; }
    }
}
