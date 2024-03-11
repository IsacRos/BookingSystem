using BookingSystem.Core.Classes;
using BookingSystem.Core.DTOs;

namespace BookingSystem.Infrastructure.Services;

public interface ITableService
{
    Task<TablesDto> GetTables(string id);
    Task AddTable(string id, int capacity);
    Task DeleteTable(string restaurantId, string tableId);
}
