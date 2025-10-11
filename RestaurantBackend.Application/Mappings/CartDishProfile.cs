using AutoMapper;
using RestaurantBackend.Application.DTOs.Responses;
using RestaurantBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RestaurantBackend.Application.Mappings
{
    public class CartDishProfile : Profile
    {
        public CartDishProfile()
        {
            CreateMap<DishCart, CartDishDto>()
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Dish.Photo))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Dish.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Dish.Price))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Count * src.Dish.Price))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Count));

            CreateMap<CartDishDto, DishCart>()
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Dish, opt => opt.Ignore())
                .ForMember(dest => dest.Order, opt => opt.Ignore())
                .ForMember(dest => dest.CreateDateTime, opt => opt.Ignore())
                .ForMember(dest => dest.ModifyDateTime, opt => opt.Ignore())
                .ForMember(dest => dest.DeleteDate, opt => opt.Ignore());
        }
    }

}