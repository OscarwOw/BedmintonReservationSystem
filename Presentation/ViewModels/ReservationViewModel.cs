using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewModels
{
    public class ReservationViewModel : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
