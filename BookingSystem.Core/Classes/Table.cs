using MongoDB.Bson;

namespace BookingSystem.Core.Classes;

public class Table
{
    public ObjectId Id { get; set; }
    public required int Capacity { get; set; }
    public List<TableBookings> Bookings { get; set; } = new List<TableBookings>();
}
