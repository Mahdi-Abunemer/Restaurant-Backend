using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantBackend.Application.Interfaces.Services;

namespace RestaurantBackend.API.Controllers
{

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basket;
        private readonly IAccountService _account;

        public BasketController(
            IBasketService basket,
            IAccountService account)
        {
            _basket = basket;
            _account = account;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = await _account.ResolveUserFromAccessTokenAsync();
            var items = await _basket.GetBasketForUserAsync(user.Id);
            return Ok(items);
        }


        [HttpPost("dish/{dishId:guid}")]
        public async Task<IActionResult> AddDish([FromRoute] Guid dishId)
        {
            var user = await _account.ResolveUserFromAccessTokenAsync();
            await _basket.AddDishToBasketAsync(dishId, user.Id);
            return Ok();
        }


        [HttpDelete("dish/{dishId:guid}")]
        public async Task<IActionResult> RemoveDish([FromRoute] Guid dishId, [FromQuery] bool decrease)
        {
            var user = await _account.ResolveUserFromAccessTokenAsync();

            if (decrease)
            {
                await _basket.RemoveDishFromBasketAsync(dishId, user.Id);
                return Ok();
            }
            else
            {
                await _basket.RemoveAllOfDishAsync(dishId, user.Id);
                return Ok();
            }
        }
    }
}
