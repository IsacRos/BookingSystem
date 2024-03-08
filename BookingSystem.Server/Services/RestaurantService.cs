using BookingSystem.Server.Data;
using BookingSystem.Server.Entities;
using BookingSystem.Server.DTOs;
using MongoDB.Bson;
using BookingSystem.Server.Mappers;

namespace BookingSystem.Server.Services
{
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

        public async Task<IEnumerable<RestaurantDto>> SearchFor()
        {
            await db.SearchFor(x => )
        }
        /*        public async Task UpdateRestaurant(RestaurantRequest updatedRestaurant, string id)
                {
                    var restaurant = await _bookingDbContext.Restaurant.FirstOrDefaultAsync(x => x.Id == ObjectId.Parse(id));

                    if (restaurant is not null)
                    {
                        restaurant.Address = updatedRestaurant.Address;
                        restaurant.Name = updatedRestaurant.Name;
                        restaurant.PhoneNumber = updatedRestaurant.PhoneNumber;
                        //_bookingDbContext.Update(restaurant);
                        //_bookingDbContext.SaveChanges();
                    }
                    else throw new ArgumentException("Could not edit restaurant");
                }*/

        public async Task DeleteRestaurant(string id)
        {
            
        }

    }
}
