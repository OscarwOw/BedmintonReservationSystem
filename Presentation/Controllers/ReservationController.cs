using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ReservationController : Controller
    {
        private IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
           _reservationService = reservationService;
        }
        public IActionResult Reservation()
        {
            var today = DateTime.Today;
            var location = "Building A";

            var reservations = _reservationService.GetReservationsByDateAndLocation(today, location);
            return View(reservations);
        }
    }
}
