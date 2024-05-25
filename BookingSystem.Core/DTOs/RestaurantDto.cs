using BookingSystem.Core.Classes;

namespace BookingSystem.Core.DTOs;

public class RestaurantDto
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required Address Address { get; set; }
    public required string PhoneNumber { get; set; }
    public int AmountOfTables { get; set; }
    public int TotalCapacity { get; set; }
}
