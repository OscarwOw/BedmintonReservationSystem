using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ReservationApiController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
