using Application.BusinessLogic;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ReservationsController : Controller
    {
        private IReservationService _reservationService;
        private ILoginService _loginService;
        public ReservationsController(IReservationService reservationService, ILoginService loginService)
        {
            _reservationService = reservationService;
            _loginService = loginService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reservations(string token)
        {
            var today = DateTime.Today;
            var location = "Building A";

            var reservations = _reservationService.GetReservationsByDateAndLocation(today, location);
            if (!string.IsNullOrEmpty(token))
            {

                    if (_loginService.Authorize(token))
                    {
                        ViewBag.IsAuthorized = true;
                        Console.WriteLine("authorized");
                    }
                    else
                    {
                        Response.Headers.Add("Remove-AuthToken", "true");
                        ViewBag.IsAuthorized = false;
                        Console.WriteLine("no authorized");
                    }
            }
            else
            {
                ViewBag.IsAuthorized = false;
                Response.Headers.Add("Remove-AuthToken", "true");
            }

            return View(reservations);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MyReservations(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    if (_loginService.Authorize(token))
                    {
                        var userId = int.Parse(token.Split('t')[0]);
                        var userReservations = _reservationService.GetReservationsByUser(userId);
                        ViewBag.IsAuthorized = true;
                        return View(userReservations); 
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error in auth:", ex);
                }
            }
            ViewBag.IsAuthorized = false;
            Response.Headers.Add("Remove-AuthToken", "true");
            return View("Unauthorized");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Reservation")]
        public IActionResult Reservation(string token, int reservationId)
        {
            if (string.IsNullOrEmpty(token) || !_loginService.Authorize(token))
            {
                ViewBag.IsAuthorized = false;
                return View("Unauthorized");
            }

            var reservation = _reservationService.GetReservationById(reservationId);


            ViewBag.IsAuthorized = true;
            if (reservation.UserId != Int32.Parse(token.Split('t')[0]))
            {
                return View("Unauthorized");
            }


            return View(reservation);
        }

    }
}
