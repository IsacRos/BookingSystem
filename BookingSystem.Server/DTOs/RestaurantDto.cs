using BookingSystem.Server.Classes;
using BookingSystem.Server.Entities;
using MongoDB.Bson;

namespace BookingSystem.Server.DTOs;

public class RestaurantDto
{
    public required string Id {  get; set; }
    public required string Name { get; set; }
    public required Address Address { get; set; }
    public required string PhoneNumber { get; set; }
    public int AmountOfTables { get; set; }
    public int TotalCapacity { get; set; }
}
