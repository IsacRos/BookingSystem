using BookingSystem.Core.Classes;
using BookingSystem.Core.DTOs;
using MongoDB.Bson;

namespace BookingSystem.Core.Services;

public interface ITableService
{
    Task<IEnumerable<TableDto>> GetTables(string id);
    Task AddTable(string id, int capacity);
    Task DeleteTable(string restaurantId, string tableId);
    Task SetTableBooking(string restaurantId, Table table);
    Task<Table> FindATable(string restaurantId, int amountOfPeople);
    Task RemoveTableBooking(string restaurantId, string tableId, DateTime bookingDate);
}
