using Amazon.Runtime.Internal;
using BookingSystem.Core.Classes;
using BookingSystem.Core.DTOs;
using BookingSystem.Core.Entities;
using BookingSystem.Core.Interfaces;
using MongoDB.Bson;

namespace BookingSystem.Core.Services;

public class TableService : ITableService
{
    private readonly IMongoDb<Restaurant> db;
    public TableService(IMongoDb<Restaurant> db)
    {
        this.db = db;
    }

    public async Task<IEnumerable<TableDto>> GetTables(string id)
    {
        var restaurant = await db.GetById(id);
        if (restaurant is not null)
        {
            var tableDtos = restaurant.Tables.Select(x => new TableDto
            {
                Id = x.Id.ToString(),
                Capacity = x.Capacity,
                Bookings = x.Bookings
            });
            return tableDtos;
        }
        else throw new ArgumentException("Couldn't find restaurant");
    }

    public async Task RemoveTableBooking(string restaurantId, string tableId, DateTime bookingDate)
    {
        var restaurant = await db.GetById(restaurantId);
        var table = restaurant.Tables.FirstOrDefault(t => t.Id.ToString() == tableId) ?? throw new ArgumentException("Couldn't find table");
        var tableBooking = table.Bookings.FirstOrDefault(tb => tb.StartTime <= bookingDate && tb.EndTime >= bookingDate);
        if (tableBooking is not null)
        {
            table.Bookings.Remove(tableBooking);
            await db.Update(restaurant);
        }
    }


    public async Task AddTable(string id, int capacity)
    {
        var restaurant = await db.GetById(id);
        if (restaurant is not null)
        {
            Table table = new()
            {
                Id = ObjectId.GenerateNewId(),
                Capacity = capacity
            };
            restaurant.Tables.Add(table);
            await db.Update(restaurant);
        }
        else throw new ArgumentException("Couldn't find restaurant");
    }

    public async Task DeleteTable(string restaurantId, string tableId)
    {
        var restaurant = await db.GetById(restaurantId);
        if (restaurant is not null)
        {
            var table = GetTableById(restaurant, tableId);
            restaurant.Tables.Remove(table);
            await db.Update(restaurant);
        }
        else throw new ArgumentException("Couldn't find restaurant");
    }
    public async Task SetTableBooking(string restaurantId, Table table)
    {
        var restaurant = await db.GetById(restaurantId);
        try
        {
            if (restaurant is not null)
            {
                var oldTable = restaurant.Tables.First(t => t.Id == table.Id);
                oldTable.Bookings = table.Bookings;
                await db.Update(restaurant);
            }
        }
        catch
        {
            throw new ArgumentException("Set booking went wrong");
        }

    }
    public async Task<Table> FindATable(string restaurantId, int amountOfPeople)
    {
        var restaurant = await db.GetById(restaurantId);
        if (restaurant is null) throw new ArgumentException("Couldn't find restaurant");
        var tables = restaurant.Tables;
        var table = tables.FirstOrDefault(t => t.Capacity == amountOfPeople) ??
                    tables.FirstOrDefault(t => t.Capacity - 1 == amountOfPeople) ??
                    tables.FirstOrDefault(t => t.Capacity - 2 == amountOfPeople) ??
                    tables.FirstOrDefault(t => t.Capacity + 1 == amountOfPeople) ??
                    tables.FirstOrDefault(t => t.Capacity - 3 == amountOfPeople) ??
                    tables.FirstOrDefault(t => t.Capacity >= amountOfPeople);
        if (table is not null) return table;
        else throw new ArgumentException("No table available");
    }
    private Table GetTableById(Restaurant restaurant, string TableId)
    {
        var table = restaurant.Tables.FirstOrDefault(t => t.Id == ObjectId.Parse(TableId));
        if (table is not null) return table;
        throw new ArgumentException("Couldn't get table");
    }
}
