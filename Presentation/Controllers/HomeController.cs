﻿using Application.Interfaces;
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

        public IActionResult Index(string token)
        {
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

            return View();
        }
    }
}
