using BookingSystem.Core.DTOs;
using BookingSystem.Core.Entities;
using BookingSystem.Core.Enums;
using BookingSystem.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        public readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<ActionResult> Add(BookingRequest request)
        {
            var activeBookings = await _bookingService.GetActiveBookingsByEmail(request.CustomerEmail);
            
            if (activeBookings.Count() > 0)
            {
                return BadRequest("Oops, you already have an active booking");
            }
            var result = await _bookingService.BookTable(request);
            if (result) return Ok();
            return BadRequest();
        }
        [HttpGet("CancelBooking")]
        public async Task<ActionResult> CancelBooking(string bookingId)
        {
            await _bookingService.CancelBooking(bookingId);
            return Ok();
        }
        [HttpGet("StatusChange"), Authorize]
        public async Task<ActionResult> ChangeStatus(string bookingId, BookingStatus status)
        {
            try
            {
                await _bookingService.ChangeStatus(bookingId, status);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("RespondToBookingRequest")]
        public async Task<ActionResult> RespondToBookingRequest(string bookingId, bool response)
        {
            try
            {
                await _bookingService.RespondToBookingRequest(bookingId, response);
                return Ok();
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message); 
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookings(string restaurantId, BookingStatus? status)
        {
            var bookings = await _bookingService.GetBookings(restaurantId, status);
            return Ok(bookings);
        }
        [HttpGet("UserBookings")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetUserBookings()
        {
            var bookings = await _bookingService.GetUserBookings();
            return Ok(bookings);
        }
        [HttpGet("UserActiveBookings")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetUserActiveBookings()
        {
            var bookings = await _bookingService.GetUserActiveBookings();
            return Ok(bookings);
        }
        [HttpGet("ByEmail")] 
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetActiveBookingsByEmail(string email)
        {
            var bookings = await _bookingService.GetActiveBookingsByEmail(email);
            return Ok(bookings);
        }
        /*[HttpGet("PendingBookings")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetPendingBookings(string restaurantId, BookingStatus? status)
        {
            var bookings = await _bookingService.GetBookings(restaurantId, status);
            return Ok(bookings);
        }*/
        [HttpDelete]
        public async Task<ActionResult> DeleteBooking(string bookingId)
        {
            var result = await _bookingService.DeleteBooking(bookingId);
            if (result) return Ok();
            else return BadRequest();
        }
    }
}
