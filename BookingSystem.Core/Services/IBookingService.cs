using BookingSystem.Core.DTOs;
using BookingSystem.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Core.Services;

public interface IBookingService
{
    Task<IEnumerable<BookingDto>> GetBookings(string restaurantId, BookingStatus? status = null);
    Task<bool> BookTable(BookingRequest request);
    //Task<bool> ChangeStatus(string bookingId, BookingStatus status);
    Task<bool> DeleteBooking(string bookingId);
    Task<IEnumerable<BookingDto>> GetActiveBookingsByEmail(string email);
    Task<IEnumerable<BookingDto>> GetUserBookings();
    Task RespondToBookingRequest(string bookingId, bool response);
    Task CancelBooking(string bookingId);
    Task ChangeStatus(string bookingId, BookingStatus status);
    Task<IEnumerable<BookingDto>> GetUserActiveBookings();
}
