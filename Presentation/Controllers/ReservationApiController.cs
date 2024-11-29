using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationApiController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationApiController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public IActionResult GetReservations([FromQuery] DateTime date)
        {
            var location = "Building A"; 
            var reservations = _reservationService.GetReservationsByDateAndLocation(date, location);

            return Ok(reservations);
        }
    }
}
