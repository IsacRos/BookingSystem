using BookingSystem.Core.DTOs;
using BookingSystem.Core.Entities;
using BookingSystem.Core.Interfaces;
using BookingSystem.Core.Mappers;
using Microsoft.AspNetCore.Identity;
using System.Net.WebSockets;
using System.Security.Claims;

namespace BookingSystem.Core.Services;

public class RestaurantService : IRestaurantService
{
    private readonly IRestaurantMapper mapper;
    private readonly IMongoDb<Restaurant> db;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;

    public RestaurantService(IRestaurantMapper mapper, IMongoDb<Restaurant> db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        this.mapper = mapper;
        this.db = db;
        this.userManager = userManager;
        this.signInManager = signInManager;
    }
    public async Task AddRestaurant(RestaurantRequest request)
    {
        try
        {
            var restaurant = mapper.Map(request);
            restaurant.OwnerId = GetUser();
            await db.Insert(restaurant);
        }
        catch (ArgumentNullException)
        {
            throw;
        }
    }
    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
    {
        var restaurants = await db.GetAll();
        var restaurantDtos = mapper.Map(restaurants);
        return restaurantDtos;
    }
    public async Task<IEnumerable<RestaurantDto>> GetUserRestaurants()
    {
        var restaurants = await db.SearchFor<Guid>("OwnerId", GetUser());
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
    public async Task UpdateRestaurant(RestaurantRequest updatedRestaurant)
    {
        try
        {
            Restaurant restaurant = await db.GetById(updatedRestaurant.Id!);
            var newRestaurant = mapper.Map(updatedRestaurant, restaurant);
            await db.Update(restaurant);
        }
        catch
        {
            throw new ArgumentException("Couldn't update restaurant");
        }
    }



    private Guid GetUser()
    {
        var userId = signInManager.Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.Parse(userId ?? throw new ArgumentException("Couldn't find user"));
    }


}
