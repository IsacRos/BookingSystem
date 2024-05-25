using BookingSystem.Core.DTOs;
using BookingSystem.Core.Services;
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
        public async Task<ActionResult<IEnumerable<TableDto>>> GetTables(string restaurantId)
        {
            var tables = await _tableService.GetTables(restaurantId);
            return Ok(tables);
        }

        [HttpPost]
        public async Task<ActionResult> AddTable(TableRequest request)
        {
            await _tableService.AddTable(request.Id, request.Capacity);
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
