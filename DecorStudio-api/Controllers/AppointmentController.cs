using DecorStudio_api.DTOs;
using DecorStudio_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DecorStudio_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : Controller
    {
        private readonly AppointmentService service;

        public AppointmentController(AppointmentService service)
        {
            this.service = service;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAppointment()
        {
            try
            {
                var list = await service.GetAppointments();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all-app-from-store/{storeId}")]
        public async Task<IActionResult> GetAll(int storeId)
        {
            try
            {
                var list = await service.GetAppointmentsByStore(storeId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-by-user/{id}")]
        public async Task<IActionResult> GetUserApp(string id)
        {
            try
            {
                var app = await service.GetAppointmentsByUserId(id); 
                return Ok(app);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-app")]
        public async Task<IActionResult> AddApp([FromBody] AppointmentDto dto)
        {
            try
            {
                var app = await service.CreateAppointment(dto);
                return Ok(app);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteApp(int id)
        {
            try
            {
                var app = await service.DeleteAppointment(id);
                return Ok(app);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AppointmentDto dto)
        {
            try
            {
                var app = await service.UpdateAppointment(id, dto);
                return Ok(app);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-all-distinct")]
        public async Task<IActionResult> GetAppointmentsByStoreAndDate()
        {
            try
            {
                var list = await service.GetAppointmentsByStoreAndDate();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
