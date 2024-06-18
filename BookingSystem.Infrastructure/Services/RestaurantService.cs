using BookingSystem.Infrastructure.Data;
using BookingSystem.Core.Entities;
using BookingSystem.Core.DTOs;
using MongoDB.Bson;
using BookingSystem.Core.Mappers;

namespace BookingSystem.Infrastructure.Services;

public class RestaurantService : IRestaurantService
{
    private readonly IRestaurantMapper mapper;
    private readonly MongoDb<Restaurant> db;

    public RestaurantService(IRestaurantMapper mapper, MongoDb<Restaurant> db)
    {
        this.mapper = mapper;
        this.db = db;
    }
    public async Task AddRestaurant(RestaurantRequest request)
    {
        var restaurant = mapper.Map(request);
        await db.Insert(restaurant);
    }
    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
    {
        var restaurants = await db.GetAll();
        var restaurantDtos = mapper.Map(restaurants);
        return restaurantDtos;
    }
    public async Task<RestaurantDto?> GetRestaurantById(string id)
    {
        var restaurant = await db.GetById(id);
        var restaurantDto = mapper.Map(restaurant);
        return restaurantDto;
    }
    public async Task DeleteRestaurant(string id)
    {
        await db.Delete(id);
    }

    /*   public async Task<IEnumerable<RestaurantDto>> SearchFor()
       {
           await db.SearchFor(x => )
       }*/
    public async Task UpdateRestaurant(RestaurantRequest updatedRestaurant, string id)
    {
        try
        {
            Restaurant restaurant = await db.GetById(id);
            restaurant.PhoneNumber = updatedRestaurant.PhoneNumber ?? restaurant.PhoneNumber;
            restaurant.Address = updatedRestaurant.Address ?? restaurant.Address;
            restaurant.Name = updatedRestaurant.Name ?? restaurant.Name;
            await db.Update(restaurant);
        }
        catch
        {
            throw new ArgumentException("Couldn't update restaurant");
        }
    }



}
