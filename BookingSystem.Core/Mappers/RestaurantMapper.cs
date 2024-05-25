using BookingSystem.Core.Classes;
using BookingSystem.Core.DTOs;
using BookingSystem.Core.Entities;
using MongoDB.Bson;

namespace BookingSystem.Core.Mappers
{
    public interface IRestaurantMapper
    {
        IEnumerable<RestaurantDto> Map(IEnumerable<Restaurant> restaurants);
        RestaurantDto Map(Restaurant restaurant);
        Restaurant Map(RestaurantRequest request);
        Restaurant Map(RestaurantRequest request, Restaurant restaurant);
    }
    public class RestaurantMapper : IRestaurantMapper
    {
        public IEnumerable<RestaurantDto> Map(IEnumerable<Restaurant> restaurants)
        {
            var dtos = restaurants.Select(Map);
            return dtos;
        }

        public RestaurantDto Map(Restaurant restaurant)
        {
            return new RestaurantDto
            {
                Id = restaurant.Id.ToString(),
                Name = restaurant.Name,
                Address = restaurant.Address,
                PhoneNumber = restaurant.PhoneNumber,
                AmountOfTables = restaurant.Tables.Count(),
                TotalCapacity = restaurant.Tables.Sum(x => x.Capacity)
            };
        }

        public Restaurant Map(RestaurantRequest request)
        {
            var address = new Address()
            {
                AddressLine1 = request.AddressLine1 ?? throw new ArgumentNullException(),
                AddressLine2 = request.AddressLine2,
                AddressLine3 = request.AddressLine3,
                ZipCode = request.ZipCode ?? throw new ArgumentNullException(),
                City = request.City ?? throw new ArgumentNullException(),
                Country = request.Country ?? throw new ArgumentNullException()
            };
            return new Restaurant
            {
                Id = ObjectId.GenerateNewId(),
                Name = request.Name ?? throw new ArgumentNullException(),
                Address = address,
                PhoneNumber = request.PhoneNumber ?? throw new ArgumentNullException(),
            };
        }

        public Restaurant Map(RestaurantRequest request, Restaurant restaurant)
        {
            var address = new Address()
            {
                AddressLine1 = request.AddressLine1 ?? restaurant.Address.AddressLine1,
                AddressLine2 = request.AddressLine2 ?? restaurant.Address.AddressLine2,
                AddressLine3 = request.AddressLine3 ?? restaurant.Address.AddressLine3,
                ZipCode = request.ZipCode ?? restaurant.Address.ZipCode,
                City = request.City ?? restaurant.Address.City,
                Country = request.Country ?? restaurant.Address.Country
            };
            restaurant.Address = address;
            restaurant.PhoneNumber = request.PhoneNumber ?? restaurant.PhoneNumber;
            restaurant.Name = request.Name ?? restaurant.Name;
            return restaurant;
        }
    }
}
