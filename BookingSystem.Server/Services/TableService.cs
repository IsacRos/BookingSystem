/*using BookingSystem.Server.Classes;
using BookingSystem.Server.Data;
using BookingSystem.Server.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Runtime.CompilerServices;

namespace BookingSystem.Server.Services;

public class TableService : ITableService
{
    public readonly BookingDbContext _bookingDbContext;
    public TableService(BookingDbContext bookingDbContext) => _bookingDbContext = bookingDbContext;

    public async Task<TablesDto> GetTables(string id)
    {
        var restaurant = await _bookingDbContext.Restaurant.FirstOrDefaultAsync(x => x.Id == ObjectId.Parse(id));
        if (restaurant != null)
        {
            TablesDto tables = new()
            {
                RestaurantId = restaurant.Id.ToString(),
                RestaurantName = restaurant.Name,
                Tables = new List<Table>(restaurant.Tables)
            };
            return tables;
        }
        else throw new ArgumentException("Couldn't find restaurant");
    }

    public async Task AddTable(string id, int capacity)
    {
        var restaurant = await _bookingDbContext.Restaurant.FirstOrDefaultAsync(x => x.Id == ObjectId.Parse(id));
        if (restaurant != null)
        {
            int nextId = restaurant.Tables.Count.Equals(0) ? 1 : restaurant.Tables.Max(i => i.Id) + 1;

            Table table = new() 
            {
                Id = nextId,
                Capacity = capacity 
            };
            restaurant.Tables.Add(table);
            _bookingDbContext.Restaurant.Update(restaurant);
            await _bookingDbContext.SaveChangesAsync();
        }
        else throw new ArgumentException("Couldn't find restaurant");
    }
}
*/