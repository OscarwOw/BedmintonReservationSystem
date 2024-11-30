using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        ILoginService _loginService;
        public HomeController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Index()
        {
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
                Console.WriteLine("authheader was empty");
                ViewBag.IsAuthorized = false;
                Response.Headers.Add("Remove-AuthToken", "true");
            }
            return View();
        }
    }
}
