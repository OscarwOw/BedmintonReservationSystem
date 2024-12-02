using Application.Interfaces;
using DataAccess.Models;
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

        [HttpPost]
        public IActionResult DeleteReservation(string token, int reservationId)
        {
            bool result = _reservationService.DeleteReservation(token, reservationId);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }  
        }
        [HttpPost]
        public IActionResult AddReservation(string token, DateTime startTime, int courtId)
        {
            bool result = _reservationService.AddReservation(token, reservation);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
