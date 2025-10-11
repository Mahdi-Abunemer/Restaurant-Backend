using AutoMapper;
using RestaurantBackend.Application.DTOs.Responses;
using RestaurantBackend.Application.Interfaces.Repositories;
using RestaurantBackend.Application.Interfaces.Services;
using RestaurantBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBackend.Infrastructure.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IDishRepository _dishRepo;
        private readonly IMapper _mapper;

        public BasketService(
            IBasketRepository basketRepo,
            IDishRepository dishRepo,
            IMapper mapper)
        {
            _basketRepo = basketRepo ?? throw new ArgumentNullException(nameof(basketRepo));
            _dishRepo = dishRepo ?? throw new ArgumentNullException(nameof(dishRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<CartDishDto>> GetBasketForUserAsync(Guid userId)
        {
            var basketEntries = await _basketRepo.GetItemsByUserAsync(userId);

            return _mapper.Map<List<CartDishDto>>(basketEntries);
        }

        public async Task AddDishToBasketAsync(Guid? dishId, Guid userId)
        {
            if (!dishId.HasValue)
                throw new ArgumentNullException(nameof(dishId), "Dish id must be provided.");

            var dish = await _dishRepo.GetDishDetailsAsync(dishId.Value);
            if (dish is null)
                throw new InvalidOperationException($"Dish with id {dishId.Value} was not found.");

            var entries = await _basketRepo.GetItemsByUserAsync(userId);

            var existing = entries.FirstOrDefault(e => e.DishId == dishId.Value);

            if (existing is not null)
            {
                existing.Count += 1;
                await _basketRepo.UpdateItemAsync(existing);
                return;
            }

            var newEntry = new DishCart
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                DishId = dishId.Value,
                Count = 1,
                CreateDateTime = DateTime.UtcNow,
                Dish = dish
            };

            await _basketRepo.AddItemAsync(newEntry);
        }

        public async Task RemoveDishFromBasketAsync(Guid? dishId, Guid userId)
        {
            if (!dishId.HasValue)
                throw new ArgumentNullException(nameof(dishId), "Dish id must be provided.");

            var dish = await _dishRepo.GetDishDetailsAsync(dishId.Value);
            if (dish is null)
                throw new InvalidOperationException("Dish not found.");

            var entries = await _basketRepo.GetItemsByUserAsync(userId);
            var existing = entries.FirstOrDefault(e => e.DishId == dishId.Value);

            if (existing is null)
                throw new InvalidOperationException("Dish is not present in the user's basket.");

            if (existing.Count > 1)
            {
                existing.Count -= 1;
                await _basketRepo.UpdateItemAsync(existing);
            }
            else
            {
                await _basketRepo.RemoveItemAsync(existing);
            }
        }

        public async Task RemoveAllOfDishAsync(Guid? dishId, Guid userId)
        {
            if (!dishId.HasValue)
                throw new ArgumentNullException(nameof(dishId), "Dish id must be provided.");

            var menuItem = await _dishRepo.GetDishDetailsAsync(dishId.Value);
            if (menuItem is null)
                throw new InvalidOperationException("Dish not found.");

            var entries = await _basketRepo.GetItemsByUserAsync(userId);
            var matching = entries.Where(e => e.DishId == dishId.Value).ToList();

            if (!matching.Any())
                throw new InvalidOperationException("Dish is not present in the user's basket.");

            foreach (var line in matching)
            {
                await _basketRepo.RemoveItemAsync(line);
            }
        }

    }
}