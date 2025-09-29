using RestaurantBackend.Domain.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RestaurantBackend.Domain.Entities
{
    public class Dish
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Photo { get; set; } = null!;
        public double Price { get; set; }
        public bool IsVegetarian { get; set; }
        public float AverageRating { get; set; }
        public Category Category { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public DateTime? DeleteDate { get; set; }
        public virtual ICollection<DishCart> DishCarts { get; set; } = new List<DishCart>();
        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
