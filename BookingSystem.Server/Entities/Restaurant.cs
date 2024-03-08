using BookingSystem.Server.Classes;
using MongoDB.Bson;

namespace BookingSystem.Server.Entities;

public class Restaurant : EntityBase
{
    public required string Name { get; set; }
    public required Address Address { get; set; } 
    public required string PhoneNumber { get; set; }
    public List<Table> Tables { get; set; } = new();
}

