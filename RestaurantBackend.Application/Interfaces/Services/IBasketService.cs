using RestaurantBackend.Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBackend.Application.Interfaces.Services
{
    public interface IBasketService
    {
        Task<List<CartDishDto>> GetBasketForUserAsync(Guid userId);
        Task AddDishToBasketAsync(Guid? dishId, Guid userId);
        Task RemoveDishFromBasketAsync(Guid? dishId, Guid userId);
        Task RemoveAllOfDishAsync(Guid? dishId, Guid userId);
    }
}