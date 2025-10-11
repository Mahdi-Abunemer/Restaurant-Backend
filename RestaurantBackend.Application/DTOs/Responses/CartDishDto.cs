using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBackend.Application.DTOs.Responses
{
    public class CartDishDto
    {
        public Guid Id { get; set; }
        [Required, MinLength(1)]
        public string Name { get; set; } = null!;
        [Required]
        public double Price { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string? Photo { get; set; }
    }
}