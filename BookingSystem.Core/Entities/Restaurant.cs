using BookingSystem.Core.Classes;

namespace BookingSystem.Core.Entities;

public class Restaurant : EntityBase
{
    public required string Name { get; set; }
    public required Address Address { get; set; }
    public required string PhoneNumber { get; set; }
    public Guid? OwnerId { get; set; }
    public List<Table> Tables { get; set; } = new();
}

