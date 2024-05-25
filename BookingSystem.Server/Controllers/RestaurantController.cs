using BookingSystem.Core.DTOs;
using BookingSystem.Core.Entities;
using BookingSystem.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : Controller
    {
        public readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantInfoService)
        {
            _restaurantService = restaurantInfoService;
        }

        [HttpPost, Authorize]
        public async Task<ActionResult> Add(RestaurantRequest request)
        {
            await _restaurantService.AddRestaurant(request);
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAllRestaurants()
        {
            var restaurants = await _restaurantService.GetAllRestaurants();
            return Ok(restaurants);
        }
        [HttpGet("UserRestaurants"), Authorize]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetUserRestaurants()
        {
            var restaurants = await _restaurantService.GetUserRestaurants();
            return Ok(restaurants);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetById(string id)
        {

            var restaurants = await _restaurantService.GetRestaurantById(id);
            return Ok(restaurants);
        }

        [HttpPut]
        public async Task<ActionResult> Update(RestaurantRequest request)
        {
            await _restaurantService.UpdateRestaurant(request);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            await _restaurantService.DeleteRestaurant(id);
            return Ok();
        }

    }
}
