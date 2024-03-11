using BookingSystem.Core.Entities;
using BookingSystem.Core.DTOs;
using MongoDB.Bson;

namespace BookingSystem.Infrastructure.Services;

public interface IRestaurantService
{
    Task AddRestaurant(RestaurantRequest request);
    Task<IEnumerable<RestaurantDto>> GetAllRestaurants();
    Task<RestaurantDto?> GetRestaurantById(string id);
    Task UpdateRestaurant(RestaurantRequest updatedRestaurant, string id);
    Task DeleteRestaurant(string id);
}
