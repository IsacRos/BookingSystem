using BookingSystem.Server.Classes;

namespace BookingSystem.Server.DTOs;

public class TablesDto
{
    public required string RestaurantId { get; set; }
    public required string RestaurantName { get; set; }
    public required List<Table> Tables { get; set; }

}
