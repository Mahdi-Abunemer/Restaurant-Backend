using AutoMapper;
using RestaurantBackend.Application.DTOs.Auth;
using RestaurantBackend.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RestaurantBackend.Infrastructure.Mappings
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<ApplicationUser, AuthUserInfo>()
                .ConstructUsing(src => new AuthUserInfo(src.Id, src.FullName ?? string.Empty, src.Email ?? string.Empty));
        }
    }
}
