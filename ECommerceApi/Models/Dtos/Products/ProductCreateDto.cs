using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Models.Dtos.Products
{
    public class ProductCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
