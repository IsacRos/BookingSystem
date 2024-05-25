using BookingSystem.Core.Classes;

namespace BookingSystem.Core.DTOs
{
    public class TableDto
    {
        public required string Id { get; set; }
        public required int Capacity { get; set; }
        public List<TableBookings> Bookings { get; set; } = new List<TableBookings>();
    }
}
