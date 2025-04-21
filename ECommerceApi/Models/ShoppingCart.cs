﻿using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Models
{
    public class ShoppingCart
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public IEnumerable<ShoppingCartItem> Items { get; set; }
    }
}
