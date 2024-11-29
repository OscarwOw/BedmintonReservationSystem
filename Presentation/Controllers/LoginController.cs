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

            var result = _loginService.Login(model.Name, model.Password);
            if (result == "success")
            {
                return Ok(new { success = true, message = "Login successful!" });
            }
            else
            {
                return BadRequest(new { success = false, message = "Invalid username or password" });
            }
        }
    }
}
