using Microsoft.AspNetCore.Mvc;

namespace BedmintonReservationSystem.ViewComponents
{
    public class LoginViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {



            return View("LoginRegisterModal");
        }
    }
}
