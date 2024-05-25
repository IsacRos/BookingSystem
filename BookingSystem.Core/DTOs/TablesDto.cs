namespace BookingSystem.Core.DTOs;

public class TablesDto
{
    public required string RestaurantId { get; set; }
    public required string RestaurantName { get; set; }
    public required IEnumerable<TableDto> Tables { get; set; }

}
