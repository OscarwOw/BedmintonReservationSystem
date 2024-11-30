using Application.BusinessLogic;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ReservationController : Controller
    {
        private IReservationService _reservationService;
        private ILoginService _loginService;
        public ReservationController(IReservationService reservationService, ILoginService loginService)
        {
            _reservationService = reservationService;
            _loginService = loginService;
        }
        public IActionResult Reservation()
        {
            var today = DateTime.Today;
            var location = "Building A";

            var reservations = _reservationService.GetReservationsByDateAndLocation(today, location);

            var authHeader = Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authHeader))
            {
                try
                {
                    string[] headerParts = authHeader.Split(' ');
                    if (_loginService.Authorize(headerParts[1]))
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
                catch (Exception ex)
                {
                    Console.WriteLine("error in auth:", ex);
                    Response.Headers.Add("Remove-AuthToken", "true");
                    ViewBag.IsAuthorized = false;
                }
            }
            else
            {
                ViewBag.IsAuthorized = false;
                Response.Headers.Add("Remove-AuthToken", "true");
            }

            return View(reservations);
        }
    }
}
