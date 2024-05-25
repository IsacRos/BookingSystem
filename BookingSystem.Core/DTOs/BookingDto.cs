using BookingSystem.Core.Enums;

namespace BookingSystem.Core.DTOs;

public class BookingDto
{
    public required string Id { get; set; }
    public required string CustomerFirstName { get; set; }
    public required string CustomerLastName { get; set; }
    public required string CustomerPhone { get; set; }
    public required string CustomerEmail { get; set; }
    public required string RestaurantId { get; set; }
    public required string TableId { get; set; }
    public string RestaurantName { get; set; } = string.Empty;
    public required int AmountOfPeople { get; set; }
    public BookingStatus Status { get; set; }
    public required DateTime BookingDate { get; set; }
}
