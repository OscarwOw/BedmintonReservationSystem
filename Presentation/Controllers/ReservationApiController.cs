using Application.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels;

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

        [HttpPost("delete")]
        public IActionResult DeleteReservation([FromBody] DeleteReservationRequest request)
        {
            bool result = _reservationService.DeleteReservation(request.Token, request.ReservationId);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }  
        }
        [HttpPost("add")]
        public IActionResult AddReservation([FromBody] AddReservationRequest request)
        {
            if (!DateTime.TryParse(request.StartTime, out var parsedStartTime))
            {
                return BadRequest("Invalid date format");
            }

            Reservation reservation = new() { StartTime = parsedStartTime, CourtId = request.CourtId };
            bool result = _reservationService.AddReservation(request.Token, reservation);
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
