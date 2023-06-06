using DecorStudio_api.DTOs;
using DecorStudio_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DecorStudio_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DecorController : Controller
    {
        private readonly DecorService decorService;
        public DecorController(DecorService decorService)
        {
            this.decorService = decorService;
        }

        [HttpGet("all-decors")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await decorService.GetAllDecors();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //sav dekor iz magacina
        [HttpGet("decore-warehouse/{warehouseId}")]
        public async Task<IActionResult> GetByWarehouseId(int warehouseId)
        {
            try
            {
                var list = await decorService.GetAllDecorsFromWarehouse(warehouseId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //sav dekor iz kataloga
        [HttpGet("decore-catalog/{catalogId}")]
        public async Task<IActionResult> GetByCatalogId(int catalogId)
        {
            try
            {
                var list = await decorService.GetAllDecorsFromCatalog(catalogId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //dekor iz magacina odrednjene radnje
        [HttpGet("decore-warehouse-store/{storeId}")]
        public async Task<IActionResult> Get(int storeId)
        {
            try
            {
                var list = await decorService.GetAllDecorsFromWarehouseFromStore(storeId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-decor/{warehouseId}")]
        public async Task<IActionResult> Add(int warehouseId, [FromBody] DecorDto decor)
        {
            try
            {
                await decorService.AddDecor(warehouseId ,decor);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-decor-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var decor = await decorService.GetDecorById(id);
                return Ok(decor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-decor/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DecorDto decor)
        {
            try
            {
                await decorService.UpdateDecor(id, decor);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-decor/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await decorService.DeleteDecor(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //rezervisani dekor od strane jednog korisnika
        [HttpGet("user-reservation/{userId}")]
        public async Task<IActionResult> GetUserReservation(string userId)
        {
            try
            {
                var reservation = await decorService.GetAllDecorsFromReservation(userId);
                return Ok(reservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //dekor na kojem korisnik radi
        [HttpGet("user-working/{userId}")]
        public async Task<IActionResult> GetUserWorking(string userId)
        {
            try
            {
                var working = await decorService.GetAllDecorsFromEmployee(userId);
                return Ok(working);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
