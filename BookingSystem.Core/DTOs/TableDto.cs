using MongoDB.Bson;

namespace BookingSystem.Core.DTOs
{
    public class TableDto
    {
        public required string Id { get; set; }
        public required int Capacity { get; set; }
    }
}
