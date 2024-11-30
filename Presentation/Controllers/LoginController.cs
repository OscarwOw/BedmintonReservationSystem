using Application.Interfaces;
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

        public LoginApiController(ILoginService loginService)
        {
            _loginService = loginService;
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

        [HttpPost("Logout")]
        public IActionResult Logout([FromBody] string token)
        {
            _loginService.Logout(token);
            return Ok();
        }
    }
}
