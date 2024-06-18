using BookingSystem.Core.DTOs;
using BookingSystem.Core.Entities;

namespace BookingSystem.Core.Mappers
{
    public interface IRestaurantMapper
    {
        IEnumerable<RestaurantDto> Map(IEnumerable<Restaurant> restaurants);
        RestaurantDto Map(Restaurant restaurant);
        Restaurant Map(RestaurantRequest request);
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
            return new Restaurant
            {
                Name = request.Name!,
                Address = request.Address!,
                PhoneNumber = request.PhoneNumber!,
            };
        }
    }
}
