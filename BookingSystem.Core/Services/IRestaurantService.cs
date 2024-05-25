using BookingSystem.Core.DTOs;

namespace BookingSystem.Core.Services;

public interface IRestaurantService
{
    Task AddRestaurant(RestaurantRequest request);
    Task<IEnumerable<RestaurantDto>> GetAllRestaurants();
    Task<IEnumerable<RestaurantDto>> GetUserRestaurants();
    Task<RestaurantDto?> GetRestaurantById(string id);
    Task UpdateRestaurant(RestaurantRequest updatedRestaurant);
    Task DeleteRestaurant(string id);
}
