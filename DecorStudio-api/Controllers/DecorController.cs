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

        [HttpPost("add-decor")]
        public async Task<IActionResult> Add([FromBody] DecorDto decor)
        {
            try
            {
                await decorService.AddDecor(decor);
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
    }
}
