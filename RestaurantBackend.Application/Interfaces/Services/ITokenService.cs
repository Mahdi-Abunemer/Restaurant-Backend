using RestaurantBackend.Application.DTOs.Auth;
using RestaurantBackend.Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBackend.Application.Interfaces.Services
{
    public interface ITokenService
    {
        /// <summary>Create JWT </summary>
        AuthResponse GenerateToken(AuthUserInfo user);
    }
}