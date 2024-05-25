namespace BookingSystem.Core.DTOs
{
    public class BookingRequest
    {
        public required string RestaurantId { get; set; }
        public required string CustomerFirstName { get; set; }
        public required string CustomerLastName { get; set; }
        public required string CustomerPhone { get; set; }
        public required string CustomerEmail { get; set; }
        public required int AmountOfPeople { get; set; }
        public required int BookedMinutes { get; set; }
        public required DateTime BookingDate { get; set; }
        
    }
}
