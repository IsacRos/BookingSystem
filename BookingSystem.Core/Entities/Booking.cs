using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Core.Entities;

public class Booking
{
    public ObjectId Id { get; set; }
    public string CustomerFirstName { get; set; } = "";
    public string CustomerLastName { get; set; } = "";
    public int CustomerPhone { get; set; }
    public ObjectId RestaurantId { get; set; }

    [Required(ErrorMessage = "Date is required to make this booking")]
    [Display(Name = "Booking date")]
    public DateTime BookingDate { get; set; }
}
