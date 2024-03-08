using BookingSystem.Server.Entities;
using BookingSystem.Server.DTOs;
using MongoDB.Bson;
using BookingSystem.Server.Data;
namespace BookingSystem.Server.Services;

public interface IRestaurantService
{
    Task AddRestaurant(RestaurantRequest request);
    Task<IEnumerable<RestaurantDto>> GetAllRestaurants();
    Task<RestaurantDto?> GetRestaurantById(string id);
   /* Task EditRestaurant(RestaurantRequest updatedRestaurant, string id);
    Task DeleteRestaurant(string id);*/
}
