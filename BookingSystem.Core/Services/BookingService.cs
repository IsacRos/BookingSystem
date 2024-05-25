using Amazon.Runtime.Internal;
using BookingSystem.Core.Classes;
using BookingSystem.Core.DTOs;
using BookingSystem.Core.Entities;
using BookingSystem.Core.Enums;
using BookingSystem.Core.Interfaces;
using BookingSystem.Core.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using MongoDB.Bson;
using System.Security.Claims;


namespace BookingSystem.Core.Services;

public class BookingService : IBookingService
{
    private readonly IMongoDb<Booking> db;
    private readonly IBookingMapper mapper;
    private readonly ITableService tableService;
    private readonly IRestaurantService restaurantService;
    private readonly IEmailSender emailSender;
    private readonly SignInManager<ApplicationUser> signInManager;


    //private string filePath;
    private ProjectDirectory directory = new();

    public BookingService
        (
            IMongoDb<Booking> db, 
            IBookingMapper mapper, 
            ITableService tableService, 
            IRestaurantService restaurantService, 
            IEmailSender emailSender, 
            SignInManager<ApplicationUser> signInManager
        )
    {
        this.db = db;
        this.mapper = mapper;
        this.tableService = tableService;
        this.restaurantService = restaurantService;
        this.emailSender = emailSender;
        this.signInManager = signInManager;
        //filePath = Path.Combine(directory.GetProjectDirectory(), @"BookingSystem.Core\Items\TableBookedMessage.txt");
    }
    public async Task<bool> BookTable(BookingRequest request)
    {
        try
        {
            var booking = mapper.Map(request);
            if (booking is not null)
            {
                booking.Status = BookingStatus.Pending;
                await db.Insert(booking);
                return true;
            }
            else throw new ArgumentException("Booking wasn't added");
        }
        catch
        {
            throw;
        }
    }

    public async Task RespondToBookingRequest(string bookingId, bool response)
    {
        try
        {
            var booking = await db.GetById(bookingId);
            if (response)
            {
                var table = await tableService.FindATable(booking.RestaurantId.ToString(), booking.AmountOfPeople);
                var tableBooking = new TableBookings
                {
                    StartTime = booking.BookingDate,
                    EndTime = booking.BookingDate.AddMinutes(booking.BookedMinutes)
                };
                table.Bookings.Add(tableBooking);
                await tableService.SetTableBooking(booking.RestaurantId.ToString(), table);
                booking.TableId = table.Id;
                booking.Status = BookingStatus.Approved;
                await db.Update(booking);
                await SendBookingConfirmationEmail(booking.CustomerEmail, booking.Id.ToString());
            }
            else 
            {
                booking.Status = BookingStatus.Denied;
                await SendBookingDeniedEmail(booking.CustomerEmail);
            }
            await db.Update(booking);
        }
        catch 
        {
            throw new ArgumentException("Couldn't change bookingstatus");
        }
    }

    public async Task CancelBooking(string bookingId)
    {
        var booking = await db.GetById(bookingId);
        await tableService.RemoveTableBooking(booking.RestaurantId.ToString(), booking.TableId.ToString(), booking.BookingDate);
    }

    public async Task<IEnumerable<BookingDto>> GetBookings(string restaurantId, BookingStatus? status = null)
    {
        var bookings = await db.SearchFor<ObjectId>("RestaurantId", ObjectId.Parse(restaurantId));
        bookings = status is null ? bookings : bookings.Where(b => b.Status.Equals(status));
        var bookingDtos = mapper.Map(bookings);
        return bookingDtos;
    }

    public async Task<bool> DeleteBooking(string bookingId)
    {
        try 
        { 
            await db.Delete(bookingId);
            return true;
        }
        catch { throw; }
    }

    public async Task<IEnumerable<BookingDto>> GetActiveBookingsByEmail(string email)
    {
        var bookings = await db.SearchFor("CustomerEmail", email);
        var result = bookings
            .Where(b => (b.BookingDate >= DateTime.Now) && b.Status == BookingStatus.Pending || b.Status == BookingStatus.Approved)
            .AsEnumerable();
        IEnumerable<BookingDto> dtos = new List<BookingDto>();
        if(result.Any())
        {
            dtos = mapper.Map(result);
        }
        return dtos;
    }
    public async Task<IEnumerable<BookingDto>> GetUserBookings()
    {
        var restaurants = await restaurantService.GetUserRestaurants();
        var bookingDtos = new List<BookingDto>();
        foreach (var r in restaurants)
        {
            var bookings = await db.SearchFor<ObjectId>("RestaurantId", ObjectId.Parse(r.Id));
            var dtos = bookings.Select(b =>
            {
                var dto = mapper.Map(b);
                dto.RestaurantName = r.Name;
                return dto;
            });
            bookingDtos.AddRange(dtos);
        }

        return bookingDtos;
    }

    public async Task<IEnumerable<BookingDto>> GetUserActiveBookings()
    {
        var restaurants = await restaurantService.GetUserRestaurants();
        var bookingDtos = new List<BookingDto>();
        foreach (var r in restaurants)
        {
            var bookings = await db.SearchFor(b => 
                b.RestaurantId.ToString() == r.Id 
                && b.Status == BookingStatus.Approved
                && b.BookingDate > DateTime.Now
                );
            var dtos = bookings.Select(b =>
            {
                var dto = mapper.Map(b);
                dto.RestaurantName = r.Name;
                return dto;
            });
            bookingDtos.AddRange(dtos);
        }

        return bookingDtos;
    }
    public async Task ChangeStatus(string bookingId, BookingStatus status)
    {
        var booking = await db.GetById(bookingId);
        if (booking is not null)
        {
            booking.Status = status;
            await db.Update(booking);
            if (status != BookingStatus.Approved)
            {
                await tableService.RemoveTableBooking(booking.RestaurantId.ToString(), booking.TableId.ToString(), booking.BookingDate);
            }
        }
    }

    private async Task SendBookingConfirmationEmail(string email, string id)
    {
        var filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName, @"BookingSystem.Core\Items\TableBookedMessage.txt");
        string htmlMessage = await File.ReadAllTextAsync(filePath);
        htmlMessage = htmlMessage.Replace("IdForSwap", $"{id.ToString()}");
        await emailSender.SendEmailAsync(
            email,
            "Table booked",
            htmlMessage);
    }
    private async Task SendBookingDeniedEmail(string email)
    {
        var filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName, @"BookingSystem.Core\Items\BookingDenied.txt");
        string htmlMessage = await File.ReadAllTextAsync(filePath);
        await emailSender.SendEmailAsync(
            email,
            "Table booked",
            htmlMessage);
    }
}
