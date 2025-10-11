using RestaurantBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RestaurantBackend.Application.Interfaces.Repositories
{
    public interface IBasketRepository
    {
        Task<List<DishCart>> GetItemsByUserAsync(Guid userId);
        Task AddItemAsync(DishCart cartItem);
        Task UpdateItemAsync(DishCart cartItem);
        Task RemoveItemAsync(DishCart cartItem);
    }
}