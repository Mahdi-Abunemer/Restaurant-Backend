using RestaurantBackend.Application.DTOs.Auth;
using RestaurantBackend.Application.DTOs.Requests;
using RestaurantBackend.Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBackend.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthUserInfo> SignInAsync(UserSignInRequest request);

        Task SignOutAsync();

        Task<AuthUserInfo> SignUpAsync(UserSignUpRequest request);

        Task<UserProfileDto> GetMyProfileAsync();

        Task UpdateMyProfileAsync(UserProfileUpdateRequest update);

        Task<AuthUserInfo> ResolveUserFromAccessTokenAsync();
    }
}