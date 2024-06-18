using BookingSystem.Core.Classes;

namespace BookingSystem.Core.DTOs;

public class RestaurantRequest
{
    public string? Name { get; set; } = null;
    public Address? Address { get; set; } = null;
    public string? PhoneNumber { get; set; } = null;
}
