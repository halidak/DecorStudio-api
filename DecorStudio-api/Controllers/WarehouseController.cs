using DecorStudio_api.DTOs;
using DecorStudio_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DecorStudio_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : Controller
    {
        private readonly WarehouseService warehouseService;
        public WarehouseController(WarehouseService warehouseService)
        {
            this.warehouseService = warehouseService;
        }

        [HttpGet("all-warehouses")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await warehouseService.GetAllWarehouses();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-warehouse")]
        public async Task<IActionResult> Add([FromBody] WarehouseDto warehouse)
        {
            try
            {
                await warehouseService.AddWarehouse(warehouse);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-warehouse-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var warehouse = await warehouseService.GetWarehouseById(id);
                return Ok(warehouse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-warehouse/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] WarehouseDto warehouse)
        {
            try
            {
                await warehouseService.UpdateWarehouse(id, warehouse);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-warehouse/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await warehouseService.DeleteWarehouse(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
