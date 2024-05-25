namespace BookingSystem.Core.DTOs;

public class TableRequest
{
    public required string Id { get; set; }
    public required int Capacity { get; set; }
}
