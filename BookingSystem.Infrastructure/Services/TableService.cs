using BookingSystem.Core.Classes;
using BookingSystem.Infrastructure.Data;
using BookingSystem.Core.DTOs;
using BookingSystem.Core.Entities;
using BookingSystem.Core.Mappers;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Runtime.CompilerServices;

namespace BookingSystem.Infrastructure.Services;

public class TableService : ITableService
{
    private readonly MongoDb<Restaurant> db;
    public TableService(MongoDb<Restaurant> db)
    {
        this.db = db;
    }

    public async Task<TablesDto> GetTables(string id)
    {
        var restaurant = await db.GetById(id);
        if (restaurant is not null)
        {
            var tableDtos = restaurant.Tables.Select(x => new TableDto {
                Id = x.Id.ToString(),
                Capacity = x.Capacity,
            });
            TablesDto tables = new()
            {
                RestaurantId = restaurant.Id.ToString(),
                RestaurantName = restaurant.Name,
                Tables = tableDtos
            };
            return tables;
        }
        else throw new ArgumentException("Couldn't find restaurant");
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
            var table = restaurant.Tables.FirstOrDefault(x => x.Id == ObjectId.Parse(tableId));
            if (table is not null)
            {
                restaurant.Tables.Remove(table);
                await db.Update(restaurant);
            }
            else throw new ArgumentException("Couldn't find table");
        }
        else throw new ArgumentException("Couldn't find restaurant");
    }
}
