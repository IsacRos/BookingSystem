using BookingSystem.Core.Enums;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Core.Entities;

public class Booking : EntityBase
{
    public required string CustomerFirstName { get; set; }
    public required string CustomerLastName { get; set; }
    public required string CustomerPhone { get; set; }
    public required string CustomerEmail { get; set; }
    public required int AmountOfPeople { get; set; }
    public required int BookedMinutes { get; set; }
    public BookingStatus Status { get; set; }
    public ObjectId RestaurantId { get; set; }
    public ObjectId TableId { get; set; }

    [Required(ErrorMessage = "Date is required to make this booking")]
    [Display(Name = "Booking date")]
    public DateTime BookingDate { get; set; }
}
