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
    public class BasketRepository : IBasketRepository
    {
        private readonly ApplicationDbContext _context;

        public BasketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddItemAsync(DishCart cartItem)
        {
            await _context.DishCarts.AddAsync(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DishCart>> GetItemsByUserAsync(Guid userId)
        {
            return await _context.DishCarts
                .Where(c => c.UserId == userId && c.OrderId == null)
                .Include(c => c.Dish)
                .ToListAsync();
        }

        public async Task UpdateItemAsync(DishCart cartItem)
        {
            _context.DishCarts.Update(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveItemAsync(DishCart cartItem)
        {
            _context.DishCarts.Remove(cartItem);
            await _context.SaveChangesAsync();
        }
    }
}