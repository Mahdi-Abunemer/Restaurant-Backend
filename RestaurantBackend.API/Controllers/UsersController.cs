using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantBackend.Application.DTOs.Auth;
using RestaurantBackend.Application.DTOs.Requests;
using RestaurantBackend.Application.DTOs.Responses;
using RestaurantBackend.Application.Interfaces.Services;

namespace RestaurantBackend.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAccountService _account;
        private readonly ITokenService _tokenService;

        public UsersController(
            IAccountService account,
            ITokenService tokenService)
        {
            _account = account;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserSignUpRequest request)
        {
            if (request == null) return BadRequest("Request body is required.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            AuthUserInfo userInfo = await _account.SignUpAsync(request);

            AuthResponse auth = _tokenService.GenerateToken(userInfo);

            return Ok(auth);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserSignInRequest request)
        {
            if (request == null) return BadRequest("Request body is required.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            AuthUserInfo userInfo = await _account.SignInAsync(request);

            AuthResponse auth = _tokenService.GenerateToken(userInfo);

            return Ok(auth);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _account.SignOutAsync();
            return Ok();
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var profile = await _account.GetMyProfileAsync();
            return Ok(profile);
        }

        [HttpPut("profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UserProfileUpdateRequest update)
        {
            if (update == null) return BadRequest("Request body is required.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _account.UpdateMyProfileAsync(update);
            return NoContent();
        }
    }
}