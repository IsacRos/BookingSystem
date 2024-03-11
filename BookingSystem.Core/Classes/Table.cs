using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookingSystem.Core.Classes;

public class Table
{
    public ObjectId Id { get; set; }
    public required int Capacity { get; set; }
    public bool Available { get; set; } = true;
}
