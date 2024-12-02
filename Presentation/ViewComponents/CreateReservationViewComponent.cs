using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents
{
    public class CreateReservationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("CreateReservationModal");
        }
    }
}
