using Microsoft.EntityFrameworkCore;
using RestaurantBackend.Application.Interfaces.Repositories;
using RestaurantBackend.Domain.Entities;
using RestaurantBackend.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBackend.Infrastructure.Repositories
{
    public class DishRepository : IDishRepository
    {
        private readonly ApplicationDbContext _context;

        public DishRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Dish?> GetDishDetailsAsync(Guid dishId)
        {
            return await _context.Dishes.FirstOrDefaultAsync(d => d.Id == dishId);
        }
    }
}
