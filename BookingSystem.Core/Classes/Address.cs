namespace BookingSystem.Core.Classes
{
    public class Address
    {
        public required string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public required string ZipCode { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
    }
}
