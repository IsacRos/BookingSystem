using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookingSystem.Server.Classes;

public class Table
{
    public required int Id { get; set; }
    public required int Capacity { get; set; }
}
