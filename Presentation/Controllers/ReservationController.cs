using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Reservation()
        {
            return View();
        }
    }
}
