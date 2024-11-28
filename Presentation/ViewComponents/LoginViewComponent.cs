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


        //[HttpPost]
        //public IActionResult Login(string username, string password)
        //{
        //    var result = _loginService.Login(username, password);
        //    if (result == "Success")
        //    {
        //        return Json(new { success = true, message = "Login successful!" });
        //    }
        //    else
        //    {
        //        return Json(new { success = false, message = "Invalid credentials." });
        //    }
        //}

    }
}
