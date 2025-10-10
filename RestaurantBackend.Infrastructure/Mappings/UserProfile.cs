using AutoMapper;
using RestaurantBackend.Application.DTOs.Requests;
using RestaurantBackend.Application.DTOs.Responses;
using RestaurantBackend.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBackend.Infrastructure.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Registration
            CreateMap<UserSignUpRequest, ApplicationUser>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone));

            // ApplicationUser -> UserProfileDto
            CreateMap<ApplicationUser, UserProfileDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender));

            // Update mapping
            CreateMap<UserProfileUpdateRequest, ApplicationUser>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                .ForAllMembers(x => x.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}

