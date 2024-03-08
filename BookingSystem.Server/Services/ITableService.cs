using BookingSystem.Server.Classes;
using BookingSystem.Server.DTOs;

namespace BookingSystem.Server.Services
{
    public interface ITableService
    {
        Task<TablesDto> GetTables(string id);
        Task AddTable(string id, int capacity);
    }
}
