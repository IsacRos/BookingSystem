using BookingSystem.Core.Classes;
using BookingSystem.Core.DTOs;
using BookingSystem.Infrastructure.Services;
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
        [HttpDelete]
        public async Task<ActionResult> DeleteTable(string restaurantId, string tableId)
        {
            try
            {
                await _tableService.DeleteTable(restaurantId, tableId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
