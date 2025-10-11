using RestaurantBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBackend.Application.Interfaces
{
    public interface IDishRepository
    {
        Task<Dish?> GetDishDetailsAsync(Guid dishId);
    }
}
