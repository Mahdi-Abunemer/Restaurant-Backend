using RestaurantBackend.Domain.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBackend.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime DeliveryTime { get; set; }
        public DateTime OrderTime { get; set; }  /
        public DateTime ModifyDateTime { get; set; }
        public DateTime? DeleteDate { get; set; }
        public double Price { get; set; }
        public Status Status { get; set; }
        public Guid UserId { get; set; }
        public string Address { get; set; } = null!;
        public virtual ICollection<DishCart> DishCarts { get; set; } = new List<DishCart>();
    }
}
