using Application.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BedmintonReservationSystem.ViewComponents
{
    public class LoginViewComponent : ViewComponent
    {
        ILoginService _loginService;

        public LoginViewComponent(ILoginService loginService)
        {
            _loginService = loginService;
        }
        public IViewComponentResult Invoke()
        {



            return View("LoginRegisterModal");
        }

    }
}
