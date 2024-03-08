using BookingSystem.Server.Classes;

namespace BookingSystem.Server.DTOs;

public class RestaurantRequest
{
    public required string Name { get; set; }
    public required Address Address { get; set; }
    public required string PhoneNumber { get; set; }
}
