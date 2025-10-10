using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RestaurantBackend.Application.DTOs.Auth;
using RestaurantBackend.Application.DTOs.Requests;
using RestaurantBackend.Application.DTOs.Responses;
using RestaurantBackend.Application.Interfaces.Services;
using RestaurantBackend.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBackend.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _users;
        private readonly SignInManager<ApplicationUser> _signin;
        private readonly IHttpContextAccessor _http;
        private readonly IMapper _mapper;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _users = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signin = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _http = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<AuthUserInfo> SignInAsync(UserSignInRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var user = await _users.FindByEmailAsync(request.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid email or password.");

            var check = await _signin.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
            if (!check.Succeeded)
                throw new UnauthorizedAccessException("Invalid email or password.");

            return MapToAuthUser(user);
        }

        public async Task SignOutAsync()
        {
            await _signin.SignOutAsync();
        }

        public async Task<AuthUserInfo> SignUpAsync(UserSignUpRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var newUser = _mapper.Map<ApplicationUser>(request);
            newUser.Id = Guid.NewGuid();
            newUser.UserName = request.Email;

            var result = await _users.CreateAsync(newUser, request.Password);
            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join("; ", result.Errors.Select(e => e.Description)));

            return MapToAuthUser(newUser);
        }

        public async Task<UserProfileDto> GetMyProfileAsync()
        {
            var user = await GetCurrentUserAsync();
            return _mapper.Map<UserProfileDto>(user);
        }

        public async Task UpdateMyProfileAsync(UserProfileUpdateRequest update)
        {
            if (update == null) throw new ArgumentNullException(nameof(update));
            var user = await GetCurrentUserAsync();

            _mapper.Map(update, user);

            var result = await _users.UpdateAsync(user);
            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join("; ", result.Errors.Select(e => e.Description)));
        }

        public async Task<AuthUserInfo> ResolveUserFromAccessTokenAsync()
        {
            var user = await GetCurrentUserAsync();
            return MapToAuthUser(user);
        }

        private async Task<ApplicationUser> GetCurrentUserAsync()
        {
            var uid = _http.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(uid))
                throw new UnauthorizedAccessException("User is not authenticated.");

            var user = await _users.FindByIdAsync(uid);
            if (user == null) throw new InvalidOperationException("User not found.");

            return user;
        }

        private static AuthUserInfo MapToAuthUser(ApplicationUser user)
        {
            return new AuthUserInfo(user.Id, user.FullName ?? string.Empty, user.Email ?? string.Empty);
        }
    }
}