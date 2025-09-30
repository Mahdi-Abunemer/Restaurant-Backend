using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestaurantBackend.Domain.Entities;
using RestaurantBackend.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBackend.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishCart> DishCarts { get; set; } 
        public DbSet<Order> Orders { get; set; }
        public DbSet<Rating> Ratings { get; set; } 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Dish>()
                   .Property(d => d.Category)
                   .HasConversion<int>();

            builder.Entity<Dish>().ToTable("Dishes"); 

            string dishJson = System.IO.File.ReadAllText("D:\\aspnetcore\\RestaurantBackendSolution\\RestaurantBackend.Infrastructure\\Data\\Seed\\dishes.json");
            List<Dish>? dishList = System.Text.Json.JsonSerializer.Deserialize<List<Dish>>(dishJson);

            foreach (Dish dish in dishList!)
            {
                dish.CreateDateTime = DateTime.SpecifyKind(dish.CreateDateTime, DateTimeKind.Utc);
                dish.ModifyDateTime = DateTime.SpecifyKind(dish.ModifyDateTime, DateTimeKind.Utc);
                if (dish.DeleteDate.HasValue)
                    dish.DeleteDate = DateTime.SpecifyKind(dish.DeleteDate.Value, DateTimeKind.Utc);
            }

            foreach (Dish dish in dishList)
            {
                builder.Entity<Dish>().HasData(dish);
            }
        }
    }
}
