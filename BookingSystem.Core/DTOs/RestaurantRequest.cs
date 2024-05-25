namespace BookingSystem.Core.DTOs;

public class RestaurantRequest
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? AddressLine3 { get; set; }
    public string? ZipCode { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? PhoneNumber { get; set; }
}
