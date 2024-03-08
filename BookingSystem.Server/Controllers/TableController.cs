using BookingSystem.Server.Classes;
using BookingSystem.Server.DTOs;
using BookingSystem.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableController : Controller
    {
        public readonly ITableService _tableService;
        public TableController(IRestaurantService restaurantService, ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        public async Task<ActionResult<TablesDto>> GetTables(string RestaurantId)
        {
            var tables = await _tableService.GetTables(RestaurantId);
            return Ok(tables);
        }

        [HttpPut]
        public async Task<ActionResult> AddTable(string id, int capacity)
        {
            await _tableService.AddTable(id, capacity);
            return Ok();
        }
    }
}
