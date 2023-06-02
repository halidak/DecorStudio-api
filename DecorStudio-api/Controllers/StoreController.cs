using DecorStudio_api.DTOs;
using DecorStudio_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DecorStudio_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : Controller
    {
        private readonly StoreService storeService;
        public StoreController(StoreService storeService)
        {
            this.storeService = storeService;
        }

        [HttpGet("all-stores")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await storeService.GetAllStores();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-store")]
        public async Task<IActionResult> Add([FromBody] StoreDto store)
        {
            try
            {
                await storeService.AddStore(store);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-store-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var store = await storeService.GetStoreById(id);
                return Ok(store);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-store/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StoreDto store)
        {
            try
            {
                await storeService.UpdateStore(id, store);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-store/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await storeService.DeleteStore(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
