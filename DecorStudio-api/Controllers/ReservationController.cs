using DecorStudio_api.DTOs;
using DecorStudio_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DecorStudio_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        private readonly ReservationService reservationService;

        public ReservationController(ReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        [HttpPost("make-reservation")]
        public async Task<IActionResult> MakeReservation([FromBody] ReservationDto reservation)
        {
            try
            {
                await reservationService.MakeReservation(reservation);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
