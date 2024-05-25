using BookingSystem.Core.DTOs;
using BookingSystem.Core.Entities;
using MongoDB.Bson;

namespace BookingSystem.Core.Mappers;

public interface IBookingMapper
{
    IEnumerable<BookingDto> Map(IEnumerable<Booking> bookings);
    BookingDto Map(Booking booking);
    Booking Map(BookingRequest request);
}
public class BookingMapper : IBookingMapper
{
    public IEnumerable<BookingDto> Map(IEnumerable<Booking> bookings)
    {
        var dtos = bookings.Select(Map);
        return dtos;
    }
    public BookingDto Map(Booking booking)
    {
        return new BookingDto
        {
            Id = booking.Id.ToString(),
            CustomerFirstName = booking.CustomerFirstName,
            CustomerLastName = booking.CustomerLastName,
            CustomerPhone = booking.CustomerPhone,
            CustomerEmail = booking.CustomerEmail,
            AmountOfPeople = booking.AmountOfPeople,
            RestaurantId = booking.RestaurantId.ToString(),
            TableId = booking.TableId.ToString(),
            Status = booking.Status,
            BookingDate = booking.BookingDate,
        };
    }
    public Booking Map(BookingRequest request)
    {
        return new Booking
        {
            Id = ObjectId.GenerateNewId(),
            CustomerFirstName = request.CustomerFirstName,
            CustomerLastName = request.CustomerLastName,
            CustomerPhone = request.CustomerPhone,
            CustomerEmail = request.CustomerEmail,
            AmountOfPeople = request.AmountOfPeople,
            RestaurantId = ObjectId.Parse(request.RestaurantId),
            BookingDate = request.BookingDate,
            BookedMinutes = request.BookedMinutes
        };
    }
}
