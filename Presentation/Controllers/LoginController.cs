using Application.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginApiController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ICustomLogger _customLogger;

        public LoginApiController(ILoginService loginService, ICustomLogger customLogger)
        {
            _loginService = loginService;
            _customLogger = customLogger;
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Invalid input" });
            }

            (int, int, string) result = _loginService.Login(model.Name, model.Password, model.AuthToken); //success id authtoken
            if (result.Item1 > 0)
            {
                return Ok(new { success = true, message = "Login successful!", id = result.Item2, authToken = result.Item3 });
            }
            else
            {
                return BadRequest(new { success = false, message = "Invalid username or password" });
            }
        }
        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterViewModel model)
        {
            if(model.Password != model.RepeatPassword)
            {
                BadRequest();
            }
            User user = new() { Name = model.Name, Password = model.Password };
            if (_loginService.Register(user))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("Logout")]
        public IActionResult Logout([FromBody] string token)
        {
            _loginService.Logout(token);
            return Ok();
        }
    }
}
