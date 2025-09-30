using Microsoft.AspNetCore.Identity;
using RestaurantBackend.Domain.Entities;
using RestaurantBackend.Domain.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBackend.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; } = null!;
        public DateTime CreateDateTime { get; set; }
        public DateTime ModifyDateTime { get; set; }
        public DateTime? DeleteDate { get; set; }
        public virtual ICollection<DishCart>? DishBaskets { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<Rating>? Ratings { get; set; }
    }
}