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
            return View();
        }
    }
}
